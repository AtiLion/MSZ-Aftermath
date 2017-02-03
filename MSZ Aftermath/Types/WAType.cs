using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;

namespace MSZ_Aftermath.Types
{
    public class WAType
    {
        public EItemType iType;
        public byte[] hash;

        public float recoilMax_x;
        public float recoilMax_y;
        public float recoilMin_x;
        public float recoilMin_y;

        public float spreadAim;
        public float spreadHip;

        public float shakeMax_x;
        public float shakeMax_y;
        public float shakeMax_z;
        public float shakeMin_x;
        public float shakeMin_y;
        public float shakeMin_z;

        public float range;
    }
}
