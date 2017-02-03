using System;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;

namespace MSZ_Aftermath
{
    public class Hook : MonoBehaviour
    {
        private static GameObject mainHook = null;

        private static Main main = null;

        public static void hook(bool pMode, string title, string version, string name, string creator)
        {
            Debug.Log("Attempting hook...");
            try
            {
                if (mainHook == null || main == null)
                {
                    mainHook = new GameObject();

                    main = mainHook.AddComponent<Main>();

                    DontDestroyOnLoad(main);
                }
                Universal.isPremium = pMode;
                Universal.title = title;
                Universal.ver_unturned = version;
                Universal.name = name;
                Universal.creator = creator;
                Debug.Log("Hook successful!");
            }
            catch (Exception ex)
            {
                Debug.Log("Hook Failed!");
                Debug.LogException(ex);
            }
        }

        public static void unHook()
        {
            Debug.Log("Attempting unhook...");
            try
            {
                if (mainHook != null && main != null)
                {
                    GameObject.Destroy(mainHook);

                    mainHook = null;
                    main = null;
                }
                Debug.Log("Unhook successful!");
            }
            catch (Exception ex)
            {
                Debug.Log("Unhook Failed!");
                Debug.LogException(ex);
            }
        }
    }
}
