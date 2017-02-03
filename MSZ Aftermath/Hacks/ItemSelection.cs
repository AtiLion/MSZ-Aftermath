using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;

namespace MSZ_Aftermath.Hacks
{
    public class ItemSelection : MenuUI
    {
        public List<EUseableType> uTp = new List<EUseableType>();

        public ItemSelection()
            : base("Item Selection", false, false, false)
        {
        }

        public override void loadUI()
        {
            if (GUILayout.Button("Back"))
            {
                ComponentManager.hack_Main.sHid = false;
            }
            foreach (EUseableType etype in Enum.GetValues(typeof(EUseableType)))
            {
                if (uTp.Contains(etype))
                    GUI.color = Color.green;
                else
                    GUI.color = Color.red;
                if (GUILayout.Button(etype.ToString()))
                {
                    if (uTp.Contains(etype))
                        uTp.Remove(etype);
                    else
                        uTp.Add(etype);
                }
            }
            GUI.color = Color.white;
        }
    }
}
