using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;
using MSZ_Aftermath.Types;

namespace MSZ_Aftermath.Hacks
{
    public class Weapons : MenuUI
    {
        public bool norecoil = true;
        public bool noshake = true;
        public bool nospread = true;
        public bool nosway = true;
        public bool reachHack = false;
        public bool meleeThroughWalls = false;
        public float distance = 26f;

        private bool prv_noshake = true;
        private bool prv_norecoil = true;
        private bool prv_nospread = true;
        private bool prv_reachHack = false;
        private float prv_distance = 26f;

        private ItemAsset prv_asset;

        private List<WAType> backup_assets = new List<WAType>();

        public Weapons()
            : base("Weapons Menu", true, false, false)
        {
        }

        public void Start()
        {
            if (backup_assets.Count <= 0)
            {
                foreach (ItemAsset ast in Assets.find(EAssetType.ITEM))
                {
                    if (ast == null || ast.itemName == null)
                        continue;
                    WAType wat = new WAType();
                    wat.hash = ast.hash;
                    wat.iType = ast.type;
                    if (ast.type == EItemType.GUN)
                    {
                        wat.recoilMax_x = ((ItemGunAsset)ast).recoilMax_x;
                        wat.recoilMax_y = ((ItemGunAsset)ast).recoilMax_y;
                        wat.recoilMin_x = ((ItemGunAsset)ast).recoilMin_x;
                        wat.recoilMin_y = ((ItemGunAsset)ast).recoilMin_y;

                        wat.shakeMax_x = ((ItemGunAsset)ast).shakeMax_x;
                        wat.shakeMax_y = ((ItemGunAsset)ast).shakeMax_y;
                        wat.shakeMax_z = ((ItemGunAsset)ast).shakeMax_z;
                        wat.shakeMin_x = ((ItemGunAsset)ast).shakeMin_x;
                        wat.shakeMin_y = ((ItemGunAsset)ast).shakeMin_y;
                        wat.shakeMin_z = ((ItemGunAsset)ast).shakeMin_z;

                        wat.spreadAim = ((ItemGunAsset)ast).spreadAim;
                        wat.spreadHip = ((ItemGunAsset)ast).spreadHip;
                    }
                    else if (ast.type == EItemType.MELEE)
                    {
                        wat.range = ((ItemMeleeAsset)ast).range;
                    }
                    backup_assets.Add(wat);
                }
            }
        }

        public void hideSpread()
        {
            if (Information.player == null || Information.player.equipment == null)
                return;
            if (Information.player.equipment.isSelected)
            {
                ItemAsset ia = Information.player.equipment.asset;
                Useable us = Information.player.equipment.useable;
                if (ia == null || us == null)
                    return;
                if (ia is ItemGunAsset && us is UseableGun)
                {
                    typeof(ItemGunAsset).GetField("_spreadAim", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(ia, getOriginal(ia.hash).spreadAim);
                    typeof(ItemGunAsset).GetField("_spreadHip", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(ia, getOriginal(ia.hash).spreadHip);
                    typeof(UseableGun).GetMethod("updateCrosshair", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(Information.player.equipment.useable, new object[0]);
                }
            }
        }

        private WAType getOriginal(byte[] hash)
        {
            return Array.Find(backup_assets.ToArray(), a => a.hash == hash);
        }

        public void Update()
        {
            if (Information.player == null || Information.player.equipment == null)
                return;
            if (Information.player.equipment.isSelected)
            {
                ItemAsset ia = Information.player.equipment.asset;
                Useable us = Information.player.equipment.useable;
                if (ia == null || us == null)
                    return;
                if (ia is ItemGunAsset)
                {
                    if (prv_norecoil != norecoil || prv_asset != ia)
                    {
                        if (norecoil)
                        {
                            typeof(ItemGunAsset).GetField("_recoilMax_x", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(ia, 0f);
                            typeof(ItemGunAsset).GetField("_recoilMax_y", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(ia, 0f);
                            typeof(ItemGunAsset).GetField("_recoilMin_x", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(ia, 0f);
                            typeof(ItemGunAsset).GetField("_recoilMin_y", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(ia, 0f);
                        }
                        else
                        {
                            typeof(ItemGunAsset).GetField("_recoilMax_x", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(ia, getOriginal(ia.hash).recoilMax_x);
                            typeof(ItemGunAsset).GetField("_recoilMax_y", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(ia, getOriginal(ia.hash).recoilMax_y);
                            typeof(ItemGunAsset).GetField("_recoilMin_x", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(ia, getOriginal(ia.hash).recoilMin_x);
                            typeof(ItemGunAsset).GetField("_recoilMin_y", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(ia, getOriginal(ia.hash).recoilMin_y);
                        }
                        prv_norecoil = norecoil;
                    }

                    if ((prv_nospread != nospread || prv_asset != ia) && !Information.beingScreened)
                    {
                        if (nospread)
                        {
                            typeof(ItemGunAsset).GetField("_spreadAim", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(ia, 0f);
                            typeof(ItemGunAsset).GetField("_spreadHip", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(ia, 0f);
                        }
                        else
                        {
                            typeof(ItemGunAsset).GetField("_spreadAim", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(ia, getOriginal(ia.hash).spreadAim);
                            typeof(ItemGunAsset).GetField("_spreadHip", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(ia, getOriginal(ia.hash).spreadHip);
                        }
                        prv_nospread = nospread;
                    }

                    if (prv_noshake != noshake || prv_asset != ia)
                    {
                        if (noshake)
                        {
                            typeof(ItemGunAsset).GetField("_shakeMax_x", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(ia, 0f);
                            typeof(ItemGunAsset).GetField("_shakeMax_y", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(ia, 0f);
                            typeof(ItemGunAsset).GetField("_shakeMax_z", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(ia, 0f);
                            typeof(ItemGunAsset).GetField("_shakeMin_x", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(ia, 0f);
                            typeof(ItemGunAsset).GetField("_shakeMin_y", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(ia, 0f);
                            typeof(ItemGunAsset).GetField("_shakeMin_z", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(ia, 0f);
                        }
                        else
                        {
                            typeof(ItemGunAsset).GetField("_shakeMax_x", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(ia, getOriginal(ia.hash).shakeMax_x);
                            typeof(ItemGunAsset).GetField("_shakeMax_y", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(ia, getOriginal(ia.hash).shakeMax_y);
                            typeof(ItemGunAsset).GetField("_shakeMax_z", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(ia, getOriginal(ia.hash).shakeMax_z);
                            typeof(ItemGunAsset).GetField("_shakeMin_x", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(ia, getOriginal(ia.hash).shakeMin_x);
                            typeof(ItemGunAsset).GetField("_shakeMin_y", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(ia, getOriginal(ia.hash).shakeMin_y);
                            typeof(ItemGunAsset).GetField("_shakeMin_z", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(ia, getOriginal(ia.hash).shakeMin_z);
                        }
                        prv_noshake = noshake;
                    }

                    if (us is UseableGun)
                    {
                        if (nosway)
                        {
                            typeof(UseableGun).GetField("steadyAccuracy", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(us, 4u);
                        }

                        typeof(UseableGun).GetMethod("updateCrosshair", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(Information.player.equipment.useable, new object[0]);
                    }
                }
                else if (ia is ItemMeleeAsset)
                {
                    if (prv_reachHack != reachHack || prv_distance != distance || prv_asset != ia)
                    {
                        if (reachHack)
                        {
                            typeof(ItemWeaponAsset).GetField("_range", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(ia, distance);
                        }
                        else
                        {
                            typeof(ItemWeaponAsset).GetField("_range", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(ia, getOriginal(ia.hash).range);
                        }
                        prv_reachHack = reachHack;
                        prv_distance = distance;
                    }
                }
                prv_asset = ia;
            }
        }

        public void OnGUI()
        {
        }

        public override void loadUI()
        {
            norecoil = GUILayout.Toggle(norecoil, "No Recoil");
            noshake = GUILayout.Toggle(noshake, "No Shake");
            nospread = GUILayout.Toggle(nospread, "No Spread");
            nosway = GUILayout.Toggle(nosway, "No Sway");
            if (Universal.isPremium)
            {
                meleeThroughWalls = GUILayout.Toggle(meleeThroughWalls, "Melee Through Walls");
            }
            reachHack = GUILayout.Toggle(reachHack, "Reach Hack");
            GUILayout.Label("Reach Hack: " + distance);
            distance = (float)Math.Round(GUILayout.HorizontalSlider(distance, 1f, 26f), 0);
        }
    }
}
