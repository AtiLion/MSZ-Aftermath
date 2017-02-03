using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;
using MSZ_Aftermath.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MSZ_Aftermath.Library
{
    public class WaypointManager : MonoBehaviour
    {
        private string file = Application.persistentDataPath + @"\Waypoints.dat";

        private List<Waypoint> waypoints = new List<Waypoint>();

        public void Start()
        {
            load();
        }

        public void Update()
        {

        }

        public void OnGUI()
        {
            if (Information.beingScreened)
                return;
        }

        private void load()
        {
            if (File.Exists(file))
            {
                try
                {
                    waypoints = JsonConvert.DeserializeObject<List<Waypoint>>(File.ReadAllText(file));
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                    File.Delete(file);
                    load();
                    return;
                }
            }
        }

        private void save()
        {
            File.WriteAllText(file, JsonConvert.SerializeObject(waypoints.ToArray()));
        }

        private void create()
        {
            File.WriteAllText(file, "[]");
        }

        public void addWaypoint(string name, Vector3 pos)
        {
            waypoints.Add(new Waypoint(name, pos, Provider.server));
            save();
        }

        public void removeWaypoint(Waypoint wp)
        {
            waypoints.Remove(wp);
            save();
        }

        public Waypoint getWaypoint(string name)
        {
            return Array.Find(waypoints.ToArray(), a => a.name == name && a.server == Provider.server);
        }

        public Waypoint getWaypoint(Vector3 pos)
        {
            return Array.Find(waypoints.ToArray(), a => a.pos == pos && a.server == Provider.server);
        }

        public Waypoint[] getWaypoints()
        {
            return Array.FindAll(waypoints.ToArray(), a => a.server == Provider.server);
        }
    }
}
