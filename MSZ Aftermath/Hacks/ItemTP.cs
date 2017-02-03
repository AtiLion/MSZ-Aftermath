using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;

namespace MSZ_Aftermath.Hacks
{
    public class ItemTP : MenuUI
    {
        private InteractableItem[] items;
        private InteractableStorage[] storages;

        private Vector3 prev_pos;

        private int takeID = 0;
        private int takePos = 0;
        private bool take = false;

        private DateTime update_stuff;

        public ItemTP()
            : base("Item Teleporter", true, true, true)
        {
        }

        public void Start()
        {

        }

        public void Update()
        {
            if (update_stuff == null || (DateTime.Now - update_stuff).TotalMilliseconds >= 3000)
            {
                items = Information.getItems();

                update_stuff = DateTime.Now;
            }

            if (take)
            {
                if (takePos == 0)
                {
                    InteractableItem item = items[takeID];

                    if (item != null && item.gameObject != null)
                    {
                        Rigidbody rb = item.gameObject.GetComponent<Rigidbody>();
                        rb.useGravity = false;
                        rb.AddForce((Information.player.transform.position - item.transform.position));
                        //item.GetComponent<Collider>().enabled = false;
                    }
                }
                take = false;
            }
        }

        public void OnGUI()
        {

        }

        public override void loadUI()
        {
            if (GUILayout.Button("Return to previous position"))
            {
                if (prev_pos != null)
                {
                    Information.player.transform.position = prev_pos;
                }
            }
            for (int i = 0; i < items.Length; i++)
            {
                if (GUILayout.Button("Item: " + items[i].asset.itemName))
                {
                    takeID = i;
                    takePos = 0;
                    take = true;
                }
            }
        }
    }
}
