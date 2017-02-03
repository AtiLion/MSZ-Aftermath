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
    public class KeybindManager : MonoBehaviour
    {
        private string file = Application.persistentDataPath + @"\Keybinds.dat";

        private List<KeyBind> binds = new List<KeyBind>();

        public void Start()
        {
            load();
        }

        public void Update()
        {
        }

        public void OnGUI()
        {
            if (Event.current.type == EventType.KeyDown)
            {
                KeyCode k = Event.current.keyCode;
                if (exists(k))
                {
                    KeyBind b = get(k);
                    if (b.name == "menu")
                    {
                        Universal.isOpen = !Universal.isOpen;
                    }
                }
            }
        }

        private void load()
        {
            if (File.Exists(file))
            {
                try
                {
                    binds = JsonConvert.DeserializeObject<List<KeyBind>>(File.ReadAllText(file));
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
                set("menu", "Menu", (int)KeyCode.F1);
            }
        }

        private void save()
        {
            File.WriteAllText(file, JsonConvert.SerializeObject(binds.ToArray()));
        }

        private void set(string name, string text, int key = 0)
        {
            if (exists(name))
            {
                Array.Find(binds.ToArray(), a => a.name == name).key = (KeyCode)key;
            }
            else
            {
                binds.Add(new KeyBind(name, text, (KeyCode)key));
            }
            save();
        }

        private void check()
        {
            if (!exists("menu"))
            {
                File.Delete(file);
                load();
            }
        }

        public bool exists(string name)
        {
            return Array.Exists(binds.ToArray(), a => a.name == name);
        }

        public bool exists(KeyCode key)
        {
            return Array.Exists(binds.ToArray(), a => a.key == key);
        }

        public KeyBind get(string name)
        {
            return Array.Find(binds.ToArray(), a => a.name == name);
        }

        public KeyBind get(KeyCode key)
        {
            return Array.Find(binds.ToArray(), a => a.key == key);
        }

        public KeyBind[] getAll()
        {
            return binds.ToArray();
        }
    }
}
