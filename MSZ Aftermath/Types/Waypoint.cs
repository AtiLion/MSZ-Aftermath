using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;
using Steamworks;

namespace MSZ_Aftermath.Types
{
    public class Waypoint
    {
        public string name;
        public Vector3 pos;
        public CSteamID server;

        public Waypoint(string name, Vector3 pos, CSteamID server)
        {
            this.name = name;
            this.pos = pos;
            this.server = server;
        }
    }
}
