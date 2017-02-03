using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;

namespace MSZ_Aftermath.Hacks
{
    public class Server : MenuUI
    {
        private System.Random r = new System.Random();

        private bool CcrashServer = false;
        private bool spam = false;
        private string spamText = "I choked on a bannana once!";
        private string spawnItem = "458";

        public Server()
            : base("Server Menu", true, true, false)
        {
        }

        public void Update()
        {
            if (CcrashServer)
            {
                ChatManager.sendChat(EChatMode.GLOBAL, "/i " + spawnItem);
            }
            if (spam)
            {
                ChatManager.sendChat(EChatMode.GLOBAL, spamText);
            }
        }

        public override void loadUI()
        {
            if (GUILayout.Button("Recheck Status"))
            {
                Vector3 back = Information.player.transform.position;
                Vector3 vx = Information.player.transform.position;
                vx.y = 100f;
                Information.player.transform.position = vx;
                Information.player.transform.position = back;
            }
            CcrashServer = GUILayout.Toggle(CcrashServer, "Creative Server Crash");
            GUILayout.Label("Item to spawn:");
            spawnItem = GUILayout.TextField(spawnItem);
            spam = GUILayout.Toggle(spam, "Spam");
            spamText = GUILayout.TextField(spamText);
        }
    }
}
