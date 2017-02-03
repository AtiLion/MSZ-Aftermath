using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;

namespace MSZ_Aftermath.Hacks
{
    public class World : MenuUI
    {
        private bool norain = false;
        private bool nosnow = false;

        public World()
            : base("World Menu", true, false, false)
        {
        }

        public void Update()
        {
            if (norain)
            {
                LevelLighting.rainyness = ELightingRain.NONE;
            }

            if (nosnow)
            {
                LevelLighting.snowLevel = 0f;
                RenderSettings.fogDensity = 0f;
                typeof(LevelLighting).GetField("isSnow", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, false);
                typeof(LevelLighting).GetField("snownyess", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, 0f);
            }
        }

        public override void loadUI()
        {
            norain = GUILayout.Toggle(norain, "No Rain");
            nosnow = GUILayout.Toggle(nosnow, "No Snow");
            GUILayout.Label("Time: " + LightingManager.time);
            LightingManager.time = (uint)Math.Round(GUILayout.HorizontalSlider((float)LightingManager.time, (float)0u, (float)3600u));
        }
    }
}
