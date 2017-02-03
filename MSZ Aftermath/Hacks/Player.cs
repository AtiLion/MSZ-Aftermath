using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;

namespace MSZ_Aftermath.Hacks
{
    public class Player : MenuUI
    {
        private bool auto_disconnect = false;
        private byte auto_disconnect_health = 1;
        private bool unlimitedStamina = false;
        private bool noclip = false;
        private bool noBrokenBones = false;
        private bool ghostMode = false; // Not done
        private bool noDrown = false; // Not done

        public Player()
            : base("Player Menu", true, false, false)
        {
        }

        public void Start()
        {
        }

        public void Update()
        {
            if (auto_disconnect)
            {
                if (Information.player != null && Information.player.life != null && Information.player.life.health <= auto_disconnect_health)
                {
                    Provider.disconnect();
                }
            }

            if (unlimitedStamina && (Information.trustClient || Universal.ignoreLimit))
            {
                Information.player.life.askRest(100);
            }

            if (noBrokenBones && (Information.trustClient || Universal.ignoreLimit))
            {
                Information.player.life.tellBroken(Provider.server, false);
            }

            if (noclip && (Information.trustClient || Universal.ignoreLimit))
            {
                //Physics.IgnoreLayerCollision(RayMasks.PLAYER, RayMasks.DAMAGE_CLIENT, true);
            }
            else
            {
                //Physics.IgnoreLayerCollision(RayMasks.PLAYER, RayMasks.DAMAGE_CLIENT, false);
            }

            if (ghostMode && (Information.trustClient || Universal.ignoreLimit))
            {

            }
        }

        public override void loadUI()
        {
            if (Universal.inDebug)
            {
                GUILayout.Label("Trusts client: " + (Information.trustClient ? "Yes" : "No"));
                GUILayout.Label("Trusts client 2: " + (Information.player.input.recov == -1 ? "Yes" : "No"));
            }
            if (Universal.isPremium)
            {
                if (Information.trustClient || Universal.ignoreLimit)
                {
                    noclip = GUILayout.Toggle(noclip, "Noclip");
                    unlimitedStamina = GUILayout.Toggle(unlimitedStamina, "Unlimited Stamina");
                    noBrokenBones = GUILayout.Toggle(noBrokenBones, "No Broken Bones");
                    ghostMode = GUILayout.Toggle(ghostMode, "Ghost Mode");
                    noDrown = GUILayout.Toggle(noDrown, "No Drown");
                }
                auto_disconnect = GUILayout.Toggle(auto_disconnect, "Auto Disconnect");
                GUILayout.Label("Disconnect Health: " + (int)auto_disconnect_health);
                auto_disconnect_health = (byte)GUILayout.HorizontalSlider((float)auto_disconnect_health, 1f, 99f);
            }
            if (GUILayout.Button("Drop All Items"))
                for (byte i = 0; i < PlayerInventory.PAGES - 1; i++)
                    if (Information.player.inventory.getItemCount(i) > 0)
                        for (byte a = 0; a < Information.player.inventory.getHeight(i); a++)
                            for (byte b = 0; b < Information.player.inventory.getWidth(i); b++)
                                Information.player.inventory.sendDropItem(i, b, a);
        }
    }
}
