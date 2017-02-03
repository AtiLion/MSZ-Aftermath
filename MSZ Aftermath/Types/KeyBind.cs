using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MSZ_Aftermath.Types
{
    public class KeyBind
    {
        public string name;
        public string text;
        public KeyCode key;
        public bool getting;

        public KeyBind(string name, string text, KeyCode key)
        {
            this.name = name;
            this.text = text;
            this.key = key;
            this.getting = false;
        }
    }
}
