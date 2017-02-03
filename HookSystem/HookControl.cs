using System;
using System.Reflection;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;

namespace HookSystem
{
    public class HookControl : MonoBehaviour
    {
        private static Thread t = new Thread(new ThreadStart(inject));
        private static AppDomain domain = AppDomain.CurrentDomain;

        public static void hook()
        {
            if (Provider.isServer)
                return;
            if (!Vars.running)
            {
                Debug.developerConsoleVisible = true;
                Debug.Log("Hooking domain...");
                domain.AssemblyLoad += new AssemblyLoadEventHandler(asm_loaded);
                Debug.Log("Hook complete!");
                Debug.Log("Creating thread...");
                try
                {
                    t.Start();
                    Vars.running = true;
                    Debug.Log("Thread created!");
                }
                catch (Exception ex)
                {
                    Debug.LogError("Failed to create thread!");
                    Debug.LogException(ex);
                }
            }
        }

        public static void inject()
        {
            Debug.Log("Injecting hook...");
            try
            {
                while (true)
                {
                    Thread.Sleep(2000);
                    if (Vars.systemObject == null || Vars.controller == null)
                    {
                        Debug.Log("Adding GameObject...");
                        Vars.systemObject = new GameObject();
                        Debug.Log("GameObject added!");
                        Debug.Log("Attaching hook to GameObject...");
                        Vars.controller = Vars.systemObject.AddComponent<Control>();
                        Debug.Log("Attached hook!");
                        Debug.Log("Preventing removing of hook....");
                        DontDestroyOnLoad(Vars.systemObject);
                        DontDestroyOnLoad(Vars.controller);
                        Debug.Log("Hook removal prevented!");
                        Debug.Log("Hook injected!");
                    }
                    Thread.Sleep(5000);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Failed to inject hook!");
                Debug.LogException(ex);
            }
        }

        public static void reload()
        {
            t.Abort();
            GameObject.Destroy(Vars.systemObject);
            Vars.running = false;
            Vars.controller = null;
            Vars.systemObject = null;
            hook();
        }

        private static void asm_loaded(object sender, AssemblyLoadEventArgs args)
        {
            Debug.Log("Loaded: " + args.LoadedAssembly.FullName);
        }
    }
}
