using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;
using MSZ_Aftermath.Library;
using HighlightingSystem;
using MSZ_Aftermath.Types;

namespace MSZ_Aftermath.Library
{
    public class ASManager : MonoBehaviour
    {
        private DateTime lastScreenshot;
        private DrawManager dManager;

        public void Start()
        {
            lastScreenshot = DateTime.Now;
            dManager = ComponentManager.draw_manager;
        }

        public void Update()
        {
            if (Injections.Overrides.PL.tmp_screen)
            {
                Information.beingScreened = true;
                if (dManager.highlights.Count > 0)
                {
                    foreach (HighlightType g in dManager.highlights)
                    {
                        if (g.h != null)
                        {
                            GameObject.Destroy(g.h);
                        }
                        dManager.highlights.Remove(g);
                    }
                }
                LevelLighting.vision = ELightingVision.NONE;
                LevelLighting.updateLighting();
                LevelLighting.updateLocal();
                PlayerLifeUI.updateGrayscale();
                ComponentManager.hack_Weapons.hideSpread();

                Player.player.StartCoroutine("takeScreenshot");
                lastScreenshot = DateTime.Now;
                Injections.Overrides.PL.tmp_screen = false;
            }

            if (Information.beingScreened && !Injections.Overrides.PL.tmp_screen && ((DateTime.Now - lastScreenshot).TotalMilliseconds > 3000))
            {
                Information.beingScreened = false;
                string say = "";
                if (Injections.Overrides.PL.tmp_calls > 5)
                    say = "Someone has used Observetory on you!";
                else
                    say = "Someone has used Spy on you!";
                ChatManager.list(Provider.client, EChatMode.SAY, Color.magenta, say);
                Injections.Overrides.PL.tmp_calls = 0;
            }
        }
    }
}
