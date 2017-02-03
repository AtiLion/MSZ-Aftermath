using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;

namespace MSZ_Aftermath.Hacks
{
    public class Main : MonoBehaviour
    {
        private string search = "";
        private string prevSearch = "";
        private bool prevTrust = false;
        private bool prevIgnore = false;
        private Vector2 scroll_Menu;
        private Vector2 scroll_Menus;
        private Vector2 scroll_System;
        private int fSize;

        private Library.SettingManager settings;

        private List<MenuUI> uis = new List<MenuUI>();
        private List<MenuUI> selUIs = new List<MenuUI>();
        private List<MenuUI> hidUIs = new List<MenuUI>();
        private List<string> texts = new List<string>();
        private int pos = 0;
        public int hPos = 0;
        public bool sHid = false;
        private bool one = true;

        public void toggle()
        {
            Universal.isOpen = !Universal.isOpen;
            if (!Universal.isOpen)
            {
                PlayerUI.window.showCursor = false;
            }
            else
            {
                PlayerUI.window.showCursor = true;
            }
        }

        public void Start()
        {
            settings = ComponentManager.setting_manager;

            uis.Add(ComponentManager.hack_Player);
            uis.Add(ComponentManager.hack_Aimbot);
            uis.Add(ComponentManager.hack_Triggerbot);
            uis.Add(ComponentManager.hack_ESP);
            uis.Add(ComponentManager.hack_Weapons);
            uis.Add(ComponentManager.hack_Vision);
            uis.Add(ComponentManager.hack_World);
            uis.Add(ComponentManager.hack_Server);
            uis.Add(ComponentManager.hack_AutoItemPickup);
            uis.Add(ComponentManager.hack_Vehicle);
            uis.Add(ComponentManager.hack_PlayerTP);
            uis.Add(ComponentManager.hack_ItemGetter);
            uis.Add(ComponentManager.hack_Waypoints);
            uis.Add(ComponentManager.hack_WaypointTR);
            uis.Add(ComponentManager.hack_ItemSelection);
            uis.Add(ComponentManager.hack_ItemIDFilter);
        }

        private string enc(string v, int s)
        {
            char[] array = v.ToCharArray();
            string[] codes = new string[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                codes[i] = ((int)array[i] ^ s).ToString();
            }
            return string.Join(",", codes);
        }

        public void Update()
        {
            if (Event.current.type == EventType.repaint)
            {
                if (search != prevSearch || selUIs.Count == 0 || Information.trustClient != prevTrust || Universal.ignoreLimit != prevIgnore)
                {
                    selUIs.Clear();
                    texts.Clear();
                    for (int i = 0; i < uis.Count; i++)
                    {
                        if (uis[i].isVisible)
                        {
                            if (((uis[i].isPremiumOnly && Universal.isPremium) || !uis[i].isPremiumOnly) && ((uis[i].isTrustedOnly && (Information.trustClient || Universal.ignoreLimit)) || !uis[i].isTrustedOnly))
                            {
                                if (search != "")
                                {
                                    if (uis[i].text.ToLower().Contains(search.ToLower()))
                                    {
                                        selUIs.Add(uis[i]);
                                        texts.Add(uis[i].text);
                                    }
                                }
                                else
                                {
                                    selUIs.Add(uis[i]);
                                    texts.Add(uis[i].text);
                                }
                            }
                        }
                        else
                        {
                            if (((uis[i].isPremiumOnly && Universal.isPremium) || !uis[i].isPremiumOnly) && ((uis[i].isTrustedOnly && (Information.trustClient || Universal.ignoreLimit)) || !uis[i].isTrustedOnly))
                            {
                                hidUIs.Add(uis[i]);
                            }
                        }
                    }
                    pos = 0;
                    prevSearch = search;
                    prevTrust = Information.trustClient;
                    prevIgnore = Universal.ignoreLimit;
                }
            }
        }

        public void OnGUI()
        {
            if (Information.beingScreened)
                return;
            try
            {
                if (one)
                {
                    fSize = GUI.skin.label.fontSize;
                    one = false;
                }
                if (Universal.isOpen)
                {
                    PlayerUI.window.showCursor = true;
                    GUI.skin.box.fontSize = 20;
                    GUI.Box(new Rect(0, 0, Screen.width, Screen.height), Universal.getTitle());
                    GUI.skin.label.fontSize = 15;
                    GUI.Label(new Rect(470, 150, 100, 20), "Search: ");
                    search = GUI.TextField(new Rect(530, 150, 400, 24), search);
                    GUI.skin.label.fontSize = fSize;
                    GUI.Box(new Rect(10, 190, Screen.width - 20, Screen.height - 200), "");
                    GUI.Box(new Rect(300, 200, Screen.width - 710, Screen.height - 220), ""); // Menu
                    GUI.Box(new Rect(20, 200, 270, Screen.height - 220), ""); // Menus
                    GUI.Box(new Rect(Screen.width - 400, 200, 380, Screen.height - 220), ""); // System

                    GUILayout.BeginArea(new Rect(20, 200, 270, Screen.height - 220));
                    scroll_Menus = GUILayout.BeginScrollView(scroll_Menus);
                    pos = GUILayout.SelectionGrid(pos, texts.ToArray(), 1);
                    GUILayout.EndScrollView();
                    GUILayout.EndArea();

                    GUILayout.BeginArea(new Rect(300, 200, Screen.width - 710, Screen.height - 220));
                    scroll_Menu = GUILayout.BeginScrollView(scroll_Menu);
                    if (sHid)
                        hidUIs[hPos].loadUI();
                    else
                        selUIs[pos].loadUI();
                    GUILayout.EndScrollView();
                    GUILayout.EndArea();

                    GUILayout.BeginArea(new Rect(Screen.width - 400, 200, 380, Screen.height - 220));
                    scroll_System = GUILayout.BeginScrollView(scroll_System);
                    GUILayout.Label(enc(Provider.client.m_SteamID.ToString(), 269));
                    if (Universal.isPremium)
                    {
                        Universal.ignoreLimit = GUILayout.Toggle(Universal.ignoreLimit, "Ignore Limit");
                    }
                    Universal.showHack = GUILayout.Toggle(Universal.showHack, "Show Hack");
                    Universal.reset_hack = GUILayout.Toggle(Universal.reset_hack, "Reset Hack");
                    Universal.instantDisconnect = GUILayout.Toggle(Universal.instantDisconnect, "Instant Disconnect");
                    Universal.altf4 = GUILayout.Toggle(Universal.altf4, "No Anti Alt+F4");
                    if (GUILayout.Button("Force Save"))
                    {
                        settings.udSys();
                    }
                    GUILayout.EndScrollView();
                    GUILayout.EndArea();
                }
            }
            catch (Exception ex)
            {
                // Nothing here
            }
        }
    }
}
