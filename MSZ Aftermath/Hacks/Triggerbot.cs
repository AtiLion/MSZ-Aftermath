using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;

namespace MSZ_Aftermath.Hacks
{
    public class Triggerbot : MenuUI
    {
        private bool triggerbot = false;
        private bool wepDistance = true;
        private bool bulletSnap = true;
        private bool trig_players = true;
        private bool trig_zombies = false;
        private bool trig_animal = false;
        private float distance = 1f;

        public Triggerbot()
            : base("Triggerbot Menu", true, false, false)
        {
        }

        public void Update()
        {
            if (triggerbot)
            {
                ItemAsset ia = Information.player.equipment.asset;
                Useable us = Information.player.equipment.useable;
                if (Information.player.equipment.isSelected && ia != null && ia is ItemWeaponAsset)
                {
                    RaycastHit hit;
                    if (Tool.getLookingAt(out hit, (wepDistance ? ((ItemWeaponAsset)ia).range : distance)))
                    {
                        if ((trig_players && DamageTool.getPlayer(hit.transform)) || (trig_zombies && DamageTool.getZombie(hit.transform)) || (trig_animal && DamageTool.getAnimal(hit.transform)))
                            attack(ia, Tool.getDistance(hit.transform.position));
                    }
                }
            }
        }

        private void attack(ItemAsset asset, float distance)
        {
            if (asset is ItemGunAsset && Provider.mode != EGameMode.EASY && bulletSnap)
            {
                ItemGunAsset iga = (ItemGunAsset)asset;
                float drop = distance - iga.ballisticSteps;
                if (drop > 0)
                    drop = ((drop / iga.ballisticTravel) * (iga.ballisticDrop + iga.ballisticForce)) + ((distance / iga.ballisticSteps) * (iga.ballisticDrop + iga.ballisticForce));
                else
                    drop = 0f;
                Information.player.look.GetType().GetField("_pitch", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(Information.player.look, Information.player.look.pitch - drop);
            }
            Information.player.equipment.GetType().GetField("prim", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(Information.player.equipment, true);
        }

        public override void loadUI()
        {
            triggerbot = GUILayout.Toggle(triggerbot, "TriggerBot");
            wepDistance = GUILayout.Toggle(wepDistance, "Use Weapon Distance");
            if (Universal.isPremium)
            {
                bulletSnap = GUILayout.Toggle(bulletSnap, "Snap To Bullet Drop");
            }
            trig_players = GUILayout.Toggle(trig_players, "Attack Players");
            trig_zombies = GUILayout.Toggle(trig_zombies, "Attack Zombies");
            trig_animal = GUILayout.Toggle(trig_animal, "Attack Animal");
            GUILayout.Label("Distance: " + distance);
            distance = GUILayout.HorizontalSlider(distance, 1f, 200f);
        }
    }
}
