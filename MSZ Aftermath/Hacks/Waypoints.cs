using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;

namespace MSZ_Aftermath.Hacks
{
    public class Waypoints : MenuUI
    {
        private string waypoint_name = "";

        private string waypoint_x = "";
        private string waypoint_y = "";
        private string waypoint_z = "";

        public Waypoints()
            : base("Waypoint Manager", true, false, false)
        {
        }

        public void Start()
        {

        }

        public void Update()
        {

        }

        public void OnGUI()
        {

        }

        public override void loadUI()
        {
            GUILayout.Label("Waypoint name: ");
            waypoint_name = GUILayout.TextField(waypoint_name);
            if (GUILayout.Button("Add waypoint here"))
            {
                ComponentManager.waypoint_manager.addWaypoint(waypoint_name, Information.player.transform.position);
            }
            GUILayout.Space(3);
            GUILayout.Label("Position X: ");
            waypoint_x = GUILayout.TextField(waypoint_x);
            GUILayout.Label("Position Y: ");
            waypoint_y = GUILayout.TextField(waypoint_y);
            GUILayout.Label("Position Z: ");
            waypoint_z = GUILayout.TextField(waypoint_z);
            if (GUILayout.Button("Add waypoint on position"))
            {
                float x;
                float y;
                float z;

                if (float.TryParse(waypoint_x, out x) && float.TryParse(waypoint_y, out y) && float.TryParse(waypoint_z, out z))
                {
                    ComponentManager.waypoint_manager.addWaypoint(waypoint_name, new Vector3(x, y, z));
                }
            }
            GUILayout.Space(3);
            if (GUILayout.Button("Remove waypoints"))
            {
                ComponentManager.hack_WaypointTR.action = 0;
                ComponentManager.hack_Main.sHid = true;
                ComponentManager.hack_Main.hPos = 0;
            }
            if ((Information.trustClient || Universal.ignoreLimit) && Universal.isPremium)
            {
                if (GUILayout.Button("Teleport to waypoint"))
                {
                    ComponentManager.hack_WaypointTR.action = 1;
                    ComponentManager.hack_Main.sHid = true;
                    ComponentManager.hack_Main.hPos = 0;
                }
            }
        }
    }
}
