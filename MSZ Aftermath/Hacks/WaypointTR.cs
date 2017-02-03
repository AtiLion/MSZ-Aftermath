using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;
using MSZ_Aftermath.Types;

namespace MSZ_Aftermath.Hacks
{
    public class WaypointTR : MenuUI
    {
        public int action = 0;

        public WaypointTR()
            : base("Waypoint Teleport/Remove", false, false, false)
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
            if (GUILayout.Button("Back"))
            {
                ComponentManager.hack_Main.sHid = false;
            }
            for (int i = 0; i < ComponentManager.waypoint_manager.getWaypoints().Length; i++)
            {
                Waypoint wp = ComponentManager.waypoint_manager.getWaypoints()[i];
                if (GUILayout.Button(wp.name))
                {
                    if (action == 0)
                        ComponentManager.waypoint_manager.removeWaypoint(wp);
                    else if (action == 1 && Information.trustClient && Universal.isPremium)
                        Information.player.transform.position = wp.pos;
                }
            }
        }
    }
}
