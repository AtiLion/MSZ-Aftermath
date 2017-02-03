using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;
using Newtonsoft.Json;

namespace MSZ_Aftermath.Library
{
    public class ItemIDManager : MonoBehaviour
    {
        private string file = Application.persistentDataPath + @"\ItemIDs.dat";

        private List<ushort> ids = new List<ushort>();

        public void Start()
        {
            load();
        }

        private void load()
        {
            if (File.Exists(file))
            {
                try
                {
                    ids = JsonConvert.DeserializeObject<List<ushort>>(File.ReadAllText(file));
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
            File.WriteAllText(file, JsonConvert.SerializeObject(ids.ToArray()));
        }

        public void addID(ushort id)
        {
            ids.Add(id);
            save();
        }

        public void removeID(ushort id)
        {
            ids.Remove(id);
            save();
        }

        public bool exists(ushort id)
        {
            return ids.Contains(id);
        }

        public ushort[] getIDs()
        {
            return ids.ToArray();
        }
    }
}
