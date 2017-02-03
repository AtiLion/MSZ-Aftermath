using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;

namespace MSZ_Aftermath.Hacks
{
    public class Vision : MenuUI
    {
        private bool nightvision_military = false;
        private bool nightvision_civilian = false;
        private bool no_vanish = false; // Not done
        private bool noflash = false;

        private bool prev_night = false;

        public Vision()
            : base("Vision Menu", true, false, false)
        {
        }

        public void Update()
        {
            if (noflash && ((Color)typeof(PlayerUI).GetField("stunColor", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).a > 0f)
            {
                Color c = (Color)typeof(PlayerUI).GetField("stunColor", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
                c.a = 0f;
                typeof(PlayerUI).GetField("stunColor", BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, c);
            }
        }

        public void OnGUI()
        {
            if (Information.beingScreened)
                return;
            if (Event.current.type == EventType.Repaint)
            {
                if (nightvision_military)
                {
                    LevelLighting.vision = ELightingVision.MILITARY;
                    LevelLighting.updateLighting();
                    LevelLighting.updateLocal();
                    PlayerLifeUI.updateGrayscale();
                    prev_night = true;
                }
                else if (nightvision_civilian)
                {
                    LevelLighting.vision = ELightingVision.CIVILIAN;
                    LevelLighting.updateLighting();
                    LevelLighting.updateLocal();
                    PlayerLifeUI.updateGrayscale();
                    prev_night = true;
                }
                else
                {
                    if (prev_night)
                    {
                        LevelLighting.vision = ELightingVision.NONE;
                        LevelLighting.updateLighting();
                        LevelLighting.updateLocal();
                        PlayerLifeUI.updateGrayscale();
                        prev_night = false;
                    }
                }
            }
        }

        public override void loadUI()
        {
            nightvision_military = GUILayout.Toggle(nightvision_military, "Military Nightvision");
            nightvision_civilian = GUILayout.Toggle(nightvision_civilian, "Civilian Nightvision");
            no_vanish = GUILayout.Toggle(no_vanish, "See Vanished Players");
            noflash = GUILayout.Toggle(noflash, "No Flashbang");
        }
    }
}
