using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;

namespace MSZ_Aftermath.Hacks
{
    public class AutoItemPickup : MenuUI
    {
        private Thread t_Pickup;
        private bool terminate = false;

        private bool AIP = false;
        private bool ignoreWalls = true;
        private bool pickupAll = false;
        private bool pickupFilter = true;
        private bool pickupID = true;
        private bool ignoreID = false;
        private bool ignoreFilter = false;

        private Vector3 cPos;
        private bool canUse = false;
        private InteractableItem[] toPickup;

        public AutoItemPickup()
            : base("Auto Item Pickup", true, false, false)
        {
        }

        public void Start()
        {
            t_Pickup = new Thread(new ThreadStart(udPickup));
            t_Pickup.Start();
        }

        public void Update()
        {
            if (Information.player != null && Information.player.transform != null && AIP)
            {
                if (canUse)
                {
                    foreach (InteractableItem item in toPickup)
                    {
                        item.use();
                    }
                    canUse = false;
                }
            }
        }

        public void OnDestroy()
        {
            terminate = true;
        }

        private void udPickup()
        {
            while (!terminate)
            {
                if (Information.player != null && Information.player.transform != null)
                {
                    if (cPos != Information.player.transform.position)
                    {
                        if (AIP)
                        {
                            toPickup = getItems();
                            canUse = true;
                        }
                        cPos = Information.player.transform.position;
                    }
                }
            }
        }

        private InteractableItem[] getItems()
        {
            Collider[] array = Physics.OverlapSphere(Information.player.look.aim.position, 10f, RayMasks.ITEM);
            List<InteractableItem> items = new List<InteractableItem>();

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != null && array[i].gameObject != null)
                {
                    InteractableItem item = array[i].GetComponent<InteractableItem>();
                    if (item != null && (Tool.isObscured(item.transform) || ignoreWalls))
                    {
                        if (pickupAll)
                        {
                            if (ignoreFilter && ComponentManager.hack_ItemSelection.uTp.Contains(item.asset.useable))
                                continue;
                            if (ignoreID && ComponentManager.itemid_manager.exists(item.asset.id))
                                continue;
                            items.Add(item);
                            continue;
                        }

                        if (pickupFilter && !ignoreFilter)
                        {
                            if (ComponentManager.hack_ItemSelection.uTp.Contains(item.asset.useable) && (!ignoreID || !ComponentManager.itemid_manager.exists(item.asset.id)))
                            {
                                items.Add(item);
                                continue;
                            }
                        }

                        if (pickupID && !ignoreID)
                        {
                            if (ComponentManager.itemid_manager.exists(item.asset.id) && (!ignoreFilter || !ComponentManager.hack_ItemSelection.uTp.Contains(item.asset.useable)))
                            {
                                items.Add(item);
                                continue;
                            }
                        }
                    }
                }
            }

            return items.ToArray();
        }

        public override void loadUI()
        {
            AIP = GUILayout.Toggle(AIP, "Auto Item Pickup");
            ignoreWalls = GUILayout.Toggle(ignoreWalls, "Ignore Walls");
            pickupAll = GUILayout.Toggle(pickupAll, "Pickup All");
            pickupFilter = GUILayout.Toggle(pickupFilter, "Pickup By Item Type");
            pickupID = GUILayout.Toggle(pickupID, "Pickup By Item ID");
            ignoreID = GUILayout.Toggle(ignoreID, "No Pickup By Item ID");
            ignoreFilter = GUILayout.Toggle(ignoreFilter, "No Pickup By Item Type");
            if (GUILayout.Button("Item Type Filter"))
            {
                ComponentManager.hack_Main.sHid = true;
                ComponentManager.hack_Main.hPos = 1;
            }
            if (GUILayout.Button("Item ID Filter"))
            {
                ComponentManager.hack_Main.sHid = true;
                ComponentManager.hack_Main.hPos = 2;
            }
        }
    }
}
