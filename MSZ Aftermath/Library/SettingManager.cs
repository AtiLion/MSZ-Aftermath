using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MSZ_Aftermath.Types;

namespace MSZ_Aftermath.Library
{
    public class SettingManager : MonoBehaviour
    {
        private string file = Application.persistentDataPath + @"\Settings.dat";

        private List<Setting> settings = new List<Setting>();

        private DateTime update_Important;

        public void Start()
        {
            load();
            udSets();
        }

        public void Update()
        {
            if (update_Important == null || (DateTime.Now - update_Important).TotalMilliseconds >= 5000)
            {
                udSys();
                update_Important = DateTime.Now;
            }
        }

        public void udSys()
        {
            if (Universal.reset_hack != (bool)get("reset_hack").value)
            {
                set("reset_hack", Universal.reset_hack);
            }
            if (Universal.showHack != (bool)get("show_hack").value)
            {
                set("show_hack", Universal.showHack);
            }
            if (Universal.ignoreLimit != (bool)get("ignore_limit").value)
            {
                set("ignore_limit", Universal.ignoreLimit);
            }
            if (Universal.instantDisconnect != (bool)get("instant_disconnect").value)
            {
                set("instant_disconnect", Universal.instantDisconnect);
            }
            if (Universal.altf4 != (bool)get("alt_f4").value)
            {
                set("alt_f4", Universal.altf4);
            }
        }

        private void udSets()
        {
            if (Universal.reset_hack != (bool)get("reset_hack").value)
            {
                Universal.reset_hack = !Universal.reset_hack;
            }
            if (Universal.showHack != (bool)get("show_hack").value)
            {
                Universal.showHack = !Universal.showHack;
            }
            if (Universal.ignoreLimit != (bool)get("ignore_limit").value)
            {
                Universal.ignoreLimit = !Universal.ignoreLimit;
            }
            if (Universal.instantDisconnect != (bool)get("instant_disconnect").value)
            {
                Universal.instantDisconnect = !Universal.instantDisconnect;
            }
            if (Universal.altf4 != (bool)get("alt_f4").value)
            {
                Universal.altf4 = !Universal.altf4;
            }
        }

        public void OnGUI()
        {
        }

        private void load()
        {
            if (File.Exists(file))
            {
                try
                {
                    settings = JsonConvert.DeserializeObject<List<Setting>>(File.ReadAllText(file));
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                    File.Delete(file);
                    load();
                    return;
                }
                check();
            }
            else
            {
                set("instant_disconnect", true);
                set("reset_hack", false);
                set("show_hack", true);
                set("ignore_limit", false);
                set("alt_f4", true);
            }
        }

        private void check()
        {
            if (!exists("instant_disconnect") || !exists("reset_hack") || !exists("show_hack") || !exists("ignore_limit") || !exists("alt_f4"))
            {
                File.Delete(file);
                load();
            }
        }

        public bool exists(string name)
        {
            return Array.Exists(settings.ToArray(), a => a.name == name);
        }

        public Setting get(string name)
        {
            return Array.Find(settings.ToArray(), a => a.name == name);
        }

        public Setting[] getAll()
        {
            return settings.ToArray();
        }

        public void set(string name, object value)
        {
            if (exists(name))
            {
                Array.Find(settings.ToArray(), a => a.name == name).value = value;
            }
            else
            {
                settings.Add(new Setting(name, value));
            }
            save();
        }

        private void save()
        {
            File.WriteAllText(file, JsonConvert.SerializeObject(settings));
        }
    }
}
