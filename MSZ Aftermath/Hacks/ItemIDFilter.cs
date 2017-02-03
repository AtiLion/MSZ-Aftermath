using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;

namespace MSZ_Aftermath.Hacks
{
    public class ItemIDFilter : MenuUI
    {
        private string sID = "";

        public ItemIDFilter() : base("Item ID Filter", false, false, false) {}

        public void Update()
        {
            if (Event.current.type == EventType.KeyDown && Event.current.character == '\n')
            {
                if (sID != "" && sID != null)
                {
                    ushort sd;
                    if (ushort.TryParse(sID, out sd))
                    {
                        ComponentManager.itemid_manager.addID(sd);
                        sID = "";
                    }
                }
            }
        }

        public override void loadUI()
        {
            GUILayout.Label("Add ID(Press ENTER after you type the ID):");
            sID = GUILayout.TextField(sID);
            if (GUILayout.Button("Back"))
            {
                ComponentManager.hack_Main.sHid = false;
            }
            foreach (ushort id in ComponentManager.itemid_manager.getIDs())
            {
                if (GUILayout.Button(id.ToString()))
                {
                    ComponentManager.itemid_manager.removeID(id);
                }
            }
        }
    }
}
