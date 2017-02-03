using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;

namespace MSZ_Aftermath.Injections
{
    public class Ctrl : MonoBehaviour
    {
        public void OnBecameVisible()
        {
            
        }

        public void OnBecameInvisible()
        {

        }

        public void OnGUI()
        {
            if (ComponentManager.hack_ESP.espEnabled)
            {
                if (ComponentManager.hack_ESP.esp_zombie && gameObject.GetComponent<Zombie>() != null)
                {
                    Tool.DrawLabel(transform.position, "Zombie");
                }
            }
        }
    }
}
