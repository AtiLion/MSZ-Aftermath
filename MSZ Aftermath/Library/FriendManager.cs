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
    public class FriendManager : MonoBehaviour
    {
        private string file = Application.persistentDataPath + @"\Friends.dat";

        private List<Friend> friends = new List<Friend>();

        public void Start()
        {
            load();
        }

        public void Update()
        {

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
                    friends = JsonConvert.DeserializeObject<List<Friend>>(File.ReadAllText(file));
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
            File.WriteAllText(file, JsonConvert.SerializeObject(friends.ToArray()));
        }

        private void create()
        {
            File.WriteAllText(file, "[]");
        }

        public bool exists(ulong id)
        {
            return Array.Exists(friends.ToArray(), a => a.ID == id);
        }

        public void addFriend(string name, string displayName, ulong id)
        {
            friends.Add(new Friend(name, displayName, id));
            save();
        }

        public void removeFriend(Friend f)
        {
            friends.Remove(f);
            save();
        }

        public Friend getFriend(ulong id)
        {
            return Array.Find(friends.ToArray(), a => a.ID == id);
        }
    }
}
