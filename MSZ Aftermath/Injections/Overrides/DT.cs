using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;

namespace MSZ_Aftermath.Injections.Overrides
{
    public class DT
    {
        public static RaycastInfo raycast(Ray ray, float range, int mask)
        {
            RaycastHit hit;
            int msk = RayMasks.ENEMY | RayMasks.ENTITY | RayMasks.VEHICLE;
            if (ComponentManager.hack_Weapons.meleeThroughWalls)
                Physics.Raycast(ray, out hit, range, msk);
            else
                Physics.Raycast(ray, out hit, range, mask);
            RaycastInfo raycastInfo = new RaycastInfo(hit);
            raycastInfo.direction = ray.direction;
            if (hit.transform != null)
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    raycastInfo.player = DamageTool.getPlayer(raycastInfo.transform);
                }
                if (hit.transform.CompareTag("Zombie"))
                {
                    raycastInfo.zombie = DamageTool.getZombie(raycastInfo.transform);
                }
                if (hit.transform.CompareTag("Animal"))
                {
                    raycastInfo.animal = DamageTool.getAnimal(raycastInfo.transform);
                }
                raycastInfo.limb = DamageTool.getLimb(raycastInfo.transform);
                if (hit.transform.CompareTag("Vehicle"))
                {
                    raycastInfo.vehicle = DamageTool.getVehicle(raycastInfo.transform);
                }
                if (raycastInfo.zombie != null && raycastInfo.zombie.isRadioactive)
                {
                    raycastInfo.material = EPhysicsMaterial.ALIEN_DYNAMIC;
                }
                else
                {
                    raycastInfo.material = DamageTool.getMaterial(hit.point, hit.transform, hit.collider);
                }
            }
            return raycastInfo;
        }
    }
}
