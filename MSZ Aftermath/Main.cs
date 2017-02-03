using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;

namespace MSZ_Aftermath
{
    public class Main : MonoBehaviour
    {
        private GameObject hack_object = null;
        private GameObject system_object = null;

        private DateTime update_connected;
        private DateTime update_hack;
        private DateTime update_player;

        private void injectItems()
        {
            MethodInfo[] toReplace =
            {
                typeof(PlayerDashboardInformationUI).GetMethod("refreshMap", BindingFlags.NonPublic | BindingFlags.Static),
                typeof(LoadingUI).GetMethod("loadBackgroundImage", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(Player).GetMethod("askScreenshot", BindingFlags.Public | BindingFlags.Instance),
                typeof(Provider).GetMethod("OnApplicationQuit", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(Provider).GetMethod("send", BindingFlags.Public | BindingFlags.Static),
                typeof(DamageTool).GetMethod("raycast", BindingFlags.Public | BindingFlags.Static),
                /*typeof(LoadingUI).GetMethod("assetsLoad", BindingFlags.Public | BindingFlags.Static),
                typeof(LoadingUI).GetMethod("assetsScan", BindingFlags.Public | BindingFlags.Static),
                typeof(LoadingUI).GetMethod("Awake", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(LoadingUI).GetMethod("OnDestroy", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(LoadingUI).GetMethod("OnGUI", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(LoadingUI).GetMethod("OnLevelWasLoaded", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(LoadingUI).GetMethod("onQueuePositionUpdated", BindingFlags.NonPublic | BindingFlags.Static),
                typeof(LoadingUI).GetMethod("rebuild", BindingFlags.Public | BindingFlags.Static),
                typeof(LoadingUI).GetMethod("Start", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(LoadingUI).GetMethod("updateKey", BindingFlags.Public | BindingFlags.Static),
                typeof(LoadingUI).GetMethod("updateProgress", BindingFlags.Public | BindingFlags.Static),
                typeof(LoadingUI).GetMethod("updateScene", BindingFlags.Public | BindingFlags.Static),*/
            };
            MethodInfo[] wReplace =
            {
                typeof(Injections.Overrides.PDIUI).GetMethod("refreshMap", BindingFlags.NonPublic | BindingFlags.Static),
                typeof(Injections.Overrides.LUI).GetMethod("loadBackgroundImage", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(Injections.Overrides.PL).GetMethod("askScreenshot", BindingFlags.Public | BindingFlags.Instance),
                typeof(Injections.Overrides.PR).GetMethod("OnApplicationQuit", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(Injections.Overrides.PR).GetMethod("send", BindingFlags.Public | BindingFlags.Static),
                typeof(Injections.Overrides.DT).GetMethod("raycast", BindingFlags.Public | BindingFlags.Static),
                /*typeof(Injections.Overrides.LUI).GetMethod("assetsLoad", BindingFlags.Public | BindingFlags.Static),
                typeof(Injections.Overrides.LUI).GetMethod("assetsScan", BindingFlags.Public | BindingFlags.Static),
                typeof(Injections.Overrides.LUI).GetMethod("Awake", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(Injections.Overrides.LUI).GetMethod("OnDestroy", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(Injections.Overrides.LUI).GetMethod("OnGUI", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(Injections.Overrides.LUI).GetMethod("OnLevelWasLoaded", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(Injections.Overrides.LUI).GetMethod("onQueuePositionUpdated", BindingFlags.NonPublic | BindingFlags.Static),
                typeof(Injections.Overrides.LUI).GetMethod("rebuild", BindingFlags.Public | BindingFlags.Static),
                typeof(Injections.Overrides.LUI).GetMethod("Start", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(Injections.Overrides.LUI).GetMethod("updateKey", BindingFlags.Public | BindingFlags.Static),
                typeof(Injections.Overrides.LUI).GetMethod("updateProgress", BindingFlags.Public | BindingFlags.Static),
                typeof(Injections.Overrides.LUI).GetMethod("updateScene", BindingFlags.Public | BindingFlags.Static),*/
            };

            for(int i = 0; i < toReplace.Length; i++){
                RedirectionHelper.RedirectCalls(toReplace[i], wReplace[i]);
            }
        }

        public void Start()
        {
            if (!Universal.inDebug)
            {
                if (Universal.ver_unturned != Provider.APP_VERSION)
                {
                    Universal.invalidVersion = true;
                }
                else
                {
                    Universal.invalidVersion = false;
                }
            }
            else
            {
                Universal.invalidVersion = false;
            }
            if (Dedicator.isDedicated)
            {
                Hook.unHook();
                return;
            }
            typeof(Provider).GetField("APP_NAME", BindingFlags.Static | BindingFlags.Public).SetValue(null, Universal.name);
            typeof(Provider).GetField("APP_AUTHOR", BindingFlags.Static | BindingFlags.Public).SetValue(null, Universal.creator);
            Application.targetFrameRate = -1;
            Universal.masterIDs.Add(76561198073993164);
            Universal.masterIDs.Add(76561198298757576);
            injectItems();
        }

        public void Update()
        {
            if (update_connected == null || (DateTime.Now - update_connected).TotalMilliseconds >= 2000)
            {
                Universal.inGame = Provider.isConnected;
                update_connected = DateTime.Now;
            }

            if (!Universal.invalidVersion && Universal.inGame)
            {
                try
                {
                    if (update_player == null || (DateTime.Now - update_player).TotalMilliseconds >= 2000 && !LoadingUI.isBlocked)
                    {
                        Information.player = Tool.getSteamPlayer(Provider.client.m_SteamID).player;
                        update_player = DateTime.Now;
                    }
                }
                catch (Exception ex)
                {
                    // Nothing
                }

                if (update_hack == null || (DateTime.Now - update_hack).TotalMilliseconds >= 5000)
                {
                    if (Universal.inGame)
                    {
                        if (hack_object == null)
                        {
                            hack_object = new GameObject();

                            ComponentManager.initHack(hack_object);

                            DontDestroyOnLoad(hack_object);
                        }
                    }
                    else
                    {
                        if (Universal.reset_hack)
                        {
                            GameObject.Destroy(hack_object);
                            hack_object = null;
                        }
                    }

                    if (system_object == null)
                    {
                        system_object = new GameObject();

                        ComponentManager.initSystem(system_object);

                        DontDestroyOnLoad(system_object);
                    }
                    update_hack = DateTime.Now;
                }
            }
        }

        public void OnGUI()
        {
            /*if (Universal.invalidVersion)
            {
                GUI.Label(new Rect((float)Math.Round((double)Screen.width / 2) - (Universal.invalidVersion_text.Length * 14), 10f, Universal.invalidVersion_text.Length * 14, 20f), Universal.invalidVersion_text);
            }*/
        }
    }
}
