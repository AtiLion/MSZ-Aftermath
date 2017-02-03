using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;

namespace MSZ_Aftermath
{
    public abstract class MenuUI : MonoBehaviour
    {
        public bool isOn = false;
        public bool isVisible = true;
        public bool isPremiumOnly = false;
        public bool isTrustedOnly = false;
        public string text = "ERROR!!";

        public MenuUI(string text, bool isVisible, bool isPremiumOnly, bool isTrustedOnly)
        {
            this.text = text;
            this.isVisible = isVisible;
            this.isPremiumOnly = isPremiumOnly;
            this.isTrustedOnly = isTrustedOnly;
        }

        public void toggle()
        {
            isOn = !isOn;
        }

        public abstract void loadUI();
    }
}
