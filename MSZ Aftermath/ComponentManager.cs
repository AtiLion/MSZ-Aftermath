using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;

namespace MSZ_Aftermath
{
    public class ComponentManager
    {
        // SYSTEM COMPONENTS
        public static Library.KeybindManager keybind_manager = null;
        public static Library.InfoManager info_manager = null;
        public static Library.SettingManager setting_manager = null;
        public static Library.FriendManager friend_manager = null;
        public static Library.DrawManager draw_manager = null;
        public static Library.SystemModifier system_modifier = null;
        public static Library.WaypointManager waypoint_manager = null;
        public static Library.CMManager chatmessage_manager = null;
        public static Library.EEManager easteregg_manager = null;
        public static Library.NetManager net_manager = null;
        public static Library.ASManager antiscreenshot_manager = null;
        public static Library.ItemIDManager itemid_manager = null;

        // HACK COMPONENTS
        public static Hacks.Main hack_Main = null;
        public static Hacks.ESP hack_ESP = null;
        public static Hacks.Player hack_Player = null;
        public static Hacks.Weapons hack_Weapons = null;
        public static Hacks.Vehicle hack_Vehicle = null;
        public static Hacks.ItemTP hack_ItemGetter = null;
        public static Hacks.WaypointTR hack_WaypointTR = null;
        public static Hacks.Waypoints hack_Waypoints = null;
        public static Hacks.Vision hack_Vision = null;
        public static Hacks.PlayerTP hack_PlayerTP = null;
        public static Hacks.World hack_World = null;
        public static Hacks.Server hack_Server = null;
        public static Hacks.Aimbot hack_Aimbot = null;
        public static Hacks.Triggerbot hack_Triggerbot = null;
        public static Hacks.AutoItemPickup hack_AutoItemPickup = null;
        public static Hacks.ItemSelection hack_ItemSelection = null;
        public static Hacks.ItemIDFilter hack_ItemIDFilter = null;

        public static void initSystem(GameObject go)
        {
            keybind_manager = go.AddComponent<Library.KeybindManager>();
            info_manager = go.AddComponent<Library.InfoManager>();
            setting_manager = go.AddComponent<Library.SettingManager>();
            friend_manager = go.AddComponent<Library.FriendManager>();
            draw_manager = go.AddComponent<Library.DrawManager>();
            system_modifier = go.AddComponent<Library.SystemModifier>();
            waypoint_manager = go.AddComponent<Library.WaypointManager>();
            chatmessage_manager = go.AddComponent<Library.CMManager>();
            easteregg_manager = go.AddComponent<Library.EEManager>();
            net_manager = go.AddComponent<Library.NetManager>();
            antiscreenshot_manager = go.AddComponent<Library.ASManager>();
            itemid_manager = go.AddComponent<Library.ItemIDManager>();
        }

        public static void initHack(GameObject go)
        {
            hack_Main = go.AddComponent<Hacks.Main>();
            hack_ESP = go.AddComponent<Hacks.ESP>();
            hack_Player = go.AddComponent<Hacks.Player>();
            hack_Weapons = go.AddComponent<Hacks.Weapons>();
            hack_Vehicle = go.AddComponent<Hacks.Vehicle>();
            hack_ItemGetter = go.AddComponent<Hacks.ItemTP>();
            hack_WaypointTR = go.AddComponent<Hacks.WaypointTR>();
            hack_Waypoints = go.AddComponent<Hacks.Waypoints>();
            hack_Vision = go.AddComponent<Hacks.Vision>();
            hack_PlayerTP = go.AddComponent<Hacks.PlayerTP>();
            hack_World = go.AddComponent<Hacks.World>();
            hack_Server = go.AddComponent<Hacks.Server>();
            hack_Aimbot = go.AddComponent<Hacks.Aimbot>();
            hack_Triggerbot = go.AddComponent<Hacks.Triggerbot>();
            hack_AutoItemPickup = go.AddComponent<Hacks.AutoItemPickup>();
            hack_ItemSelection = go.AddComponent<Hacks.ItemSelection>();
            hack_ItemIDFilter = go.AddComponent<Hacks.ItemIDFilter>();
        }
    }
}
