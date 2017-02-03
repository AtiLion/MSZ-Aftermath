using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;
using MSZ_Aftermath.Types;

namespace MSZ_Aftermath.Library
{
    public class DrawManager : MonoBehaviour
    {
        public List<DrawType> drawing = new List<DrawType>();
        public List<DrawType> drawing_esp = new List<DrawType>();
        public List<DrawType> prv_drawing = new List<DrawType>();
        public List<HighlightType> highlights = new List<HighlightType>();
        private Texture2D espTex = new Texture2D(1, 1);
        public bool d_ref = false;
        public bool rm_highlights = false;

        private DateTime update_it;

        public void Start()
        {
        }

        public void Update()
        {
            if (update_it == null || (DateTime.Now - update_it).TotalMilliseconds >= 10)
            {
                if (!d_ref)
                {
                    drawing.AddRange(drawing_esp);
                    foreach (HighlightType ht in highlights)
                    {
                        if (!ht.hasUd)
                        {
                            GameObject.Destroy(ht.h);
                            highlights.Remove(ht);
                        }
                        else
                        {
                            ht.hasUd = false;
                        }
                    }
                    d_ref = true;
                    prv_drawing.Clear();
                }
                if (rm_highlights)
                {
                    if (highlights.Count > 0)
                    {
                        foreach (HighlightType g in highlights)
                        {
                            if (g.h != null)
                            {
                                GameObject.Destroy(g.h);
                            }
                            highlights.Remove(g);
                        }
                    }
                    rm_highlights = false;
                }
                update_it = DateTime.Now;
            }
        }

        public void OnGUI()
        {
            if (Information.beingScreened)
                return;
            if (Event.current.type == EventType.Repaint)
            {
                if (drawing.Count > 0)
                {
                    prv_drawing.Clear();
                    foreach (DrawType d in drawing)
                    {
                        GUI.color = d.col;
                        if (d.names)
                            Tool.DrawLabel(d.pos, d.text);
                        if (ComponentManager.hack_ESP.draw_outline)
                            Tool.Outline(d.bds, espTex);
                    }
                    prv_drawing.AddRange(drawing);
                    drawing.Clear();
                }
                else
                {
                    foreach (DrawType d in prv_drawing)
                    {
                        GUI.color = d.col;
                        if (d.names)
                            Tool.DrawLabel(d.pos, d.text);
                        if (ComponentManager.hack_ESP.draw_outline)
                            Tool.Outline(d.bds, espTex);
                    }
                }
            }
        }
    }
}
