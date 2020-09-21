using System;
using UnityEngine;

namespace StickFightCheeto
{
    public class Loader
    {
        public static void Init()
        {
            Loader.Load = new GameObject();
            Loader.Load.AddComponent<Main>();
            UnityEngine.Object.DontDestroyOnLoad(Loader.Load);
        }
        public static void Unload()
        {
            UnityEngine.Object.Destroy(Load);
        }
        public static GameObject Load;
    }
}
