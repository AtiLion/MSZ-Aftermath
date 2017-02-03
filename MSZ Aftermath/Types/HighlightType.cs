using System;
using System.Collections.Generic;
using System.Text;
using HighlightingSystem;
using UnityEngine;

namespace MSZ_Aftermath.Types
{
    public class HighlightType
    {
        public bool hasUd;
        public Highlighter h;
        public GameObject go;

        public HighlightType(Highlighter h, GameObject go)
        {
            this.h = h;
            this.go = go;
            hasUd = true;
        }
    }
}
