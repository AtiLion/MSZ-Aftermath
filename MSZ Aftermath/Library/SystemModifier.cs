using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;

namespace MSZ_Aftermath.Library
{
    public class SystemModifier : MonoBehaviour
    {
        private bool curInstantDisconnect = false;

        public void Start()
        {
            
        }

        public void Update()
        {
            if (curInstantDisconnect != Universal.instantDisconnect)
            {
                if (Universal.instantDisconnect)
                {
                    typeof(PlayerPauseUI).GetField("TIMER_LEAVE", BindingFlags.Public | BindingFlags.Static).SetValue(null, 0f);
                }
                else
                {
                    typeof(PlayerPauseUI).GetField("TIMER_LEAVE", BindingFlags.Public | BindingFlags.Static).SetValue(null, 10f);
                }
                curInstantDisconnect = Universal.instantDisconnect;
            }
        }

        public void OnGUI()
        {

        }
    }
}
