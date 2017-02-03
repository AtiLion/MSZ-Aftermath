using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using HighlightingSystem;

namespace MSZ_Aftermath.Types
{
    public class DrawType
    {
        public string text;
        public Vector3 pos;
        public Color col;
        public bool names;
        public GameObject go;
        public Rect bds;
        public Highlighter h;

        public DrawType(string text, Vector3 pos, Color col, bool names, GameObject go, Rect bds)
        {
            this.text = text;
            this.pos = pos;
            this.col = col;
            this.names = names;
            this.go = go;
            this.bds = bds;
        }
    }
}
