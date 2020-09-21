using InControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Security.Policy;
using System.Text;
using UnityEngine;

namespace StickFightCheeto
{
    class Main : MonoBehaviour
    {
        public GameManager Gmanager;
        public ChatManager Cmanager;
        public Movement Cmovement;
        public Controller Ccontroller;
        public Fighting Cfight;
        public HealthHandler Hhandler;
        public Weapon Gweapon;
        public UnityEngine.Object[] uobjS;
        System.Random rand = new System.Random();
        void Start()
        {
            GetObjects();
        }
        void GetObjects()
        {
            Gmanager = FindObjectOfType<GameManager>();
            Cmanager = FindObjectOfType<ChatManager>();
            Cmovement = FindObjectOfType<Movement>();
            Ccontroller = FindObjectOfType<Controller>();
            Cfight = FindObjectOfType<Fighting>();
            Hhandler = FindObjectOfType<HealthHandler>();
            Gweapon = FindObjectOfType<Weapon>();
            uobjS = UnityEngine.Object.FindObjectsOfType(typeof(Head));
        }
        void Update()
        {
            if (!Gmanager.IsInLobby())
            {
                if (Input.GetKey(KeyCode.F1))
                {
                    Hhandler.health = Int32.MaxValue;
                }
                if (Input.GetKey(KeyCode.F2))
                {
                    Gmanager.ReviveAllPlayers(false);
                }
                if (Input.GetKey(KeyCode.F3))
                {
                    Ccontroller.canFly = true;
                }
                if (Input.GetKey(KeyCode.F4))
                {
                    Ccontroller.canFly = false;
                }
                if (Input.GetKey(KeyCode.K))
                {
                    Cmanager.Talk("SPED PATROL");
                }
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    Ccontroller.Jump(true);
                }
                if (Input.GetKeyDown(KeyCode.Mouse3))
                {
                    Cfight.NetworkPickUpWeapon((byte)rand.Next(0, 65));
                }
                if (Input.GetKeyDown(KeyCode.Mouse5))
                {
                    Cfight.weapon.recoil = 0f;
                    Cfight.weapon.cd = 0f;
                    Cfight.weapon.secondsOfUse = 100f;
                    Cfight.weapon.spread = 0f;
                    Cfight.weapon.startBullets = 100;
                }
            }
            else
            {
                //cry
            }
        }
        void OnGUI()
        {
            GUI.color = Color.white;
            GUI.Label(new Rect(10f, 100f, 150f, 20f), "Electric Scooter Gang");
            GUI.Label(new Rect(10f, 115f, 150f, 20f), "Health: F1");
            GUI.Label(new Rect(10f, 130f, 150f, 20f), "Revive: F2");
            GUI.Label(new Rect(10f, 145f, 150f, 20f), "Fly: F3/F4");
            GUI.Label(new Rect(10f, 160f, 150f, 20f), "Chat spam: K");
            GUI.Label(new Rect(10f, 175f, 150f, 20f), "Spam Jump: Lshift");
            GUI.Label(new Rect(10f, 190f, 150f, 20f), "Spawn Guns: Mouse 4");
            GUI.Label(new Rect(10f, 205f, 150f, 20f), "Gun shit: Mouse 5");
            foreach (Head Cjoint in uobjS)
            {
                UnityEngine.Vector3 vector = Camera.main.WorldToScreenPoint(Cjoint.transform.position);
                Line(new UnityEngine.Vector2(vector.x, (float)Screen.height - vector.y), new UnityEngine.Vector2(Screen.width/2, Screen.height/2), 2);
            }
        }
        public static void Line(UnityEngine.Vector2 lineStart, UnityEngine.Vector2 lineEnd, int thickness)
        {
            var vector = lineEnd - lineStart;
            float pivot = 57.29578f * Mathf.Atan(vector.y / vector.x);
            if (vector.x < 0f)
            {
                pivot += 180f;
            }

            if (thickness < 1)
            {
                thickness = 1;
            }

            int yOffset = (int)Mathf.Ceil((float)(thickness / 2));

            GUIUtility.RotateAroundPivot(pivot, lineStart);
            GUI.DrawTexture(new Rect(lineStart.x, lineStart.y - (float)yOffset, vector.magnitude, (float)thickness), new Texture2D(10,10));
            GUIUtility.RotateAroundPivot(-pivot, lineStart);
            // credits to 0xd3f i stole this from him
        }
    }
}
