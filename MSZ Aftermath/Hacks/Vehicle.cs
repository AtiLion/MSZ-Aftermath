using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;
using MSZ_Aftermath.Injections;

namespace MSZ_Aftermath.Hacks
{
    public class Vehicle : MenuUI
    {
        public bool fly = false;
        public bool noclip = false;
        public bool instantSpeed = false;
        public bool driveOnWater = false;
        public bool neverDrown = false;
        public float speed = 0f;

        private bool forcefield = false;

        private InteractableVehicle vehicle = null;
        private DateTime update_FF;

        public Vehicle()
            : base("Vehicle Menu", true, true, false)
        {
        }

        public void Start()
        {
        }

        public void Update()
        {
            try
            {
                if ((vehicle == null && Information.player.movement.getVehicle() != null) || vehicle != Information.player.movement.getVehicle())
                {
                    vehicle = Information.player.movement.getVehicle();
                    if (vehicle != null)
                    {
                        speed = vehicle.asset.speedMax;
                        VehicleExploiter ve = vehicle.gameObject.GetComponent<VehicleExploiter>();
                        if (ve == null)
                        {
                            ve = vehicle.gameObject.AddComponent<VehicleExploiter>();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            if (forcefield)
            {
                if (update_FF == null || (DateTime.Now - update_FF).TotalMilliseconds >= 1000)
                {
                    Collider[] c = Physics.OverlapSphere(Information.player.transform.position, 50f, RayMasks.VEHICLE);
                    for (int i = 0; i < c.Length; i++)
                    {
                        c[i].gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        c[i].gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                        c[i].gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    }
                    update_FF = DateTime.Now;
                }
            }
        }

        public void OnGUI()
        {
        }

        public override void loadUI()
        {
            fly = GUILayout.Toggle(fly, "Fly");
            noclip = GUILayout.Toggle(noclip, "Noclip");
            instantSpeed = GUILayout.Toggle(instantSpeed, "Instant Max Speed");
            forcefield = GUILayout.Toggle(forcefield, "Forcefield");
            neverDrown = GUILayout.Toggle(neverDrown, "Never Drown");
            GUILayout.Label("Max Speed: " + speed);
            speed = (float)Math.Round(GUILayout.HorizontalSlider(speed, 1f, 18f), 0);
        }
    }
}
