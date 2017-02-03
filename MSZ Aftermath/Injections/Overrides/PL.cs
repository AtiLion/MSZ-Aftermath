using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;
using Steamworks;

namespace MSZ_Aftermath.Injections.Overrides
{
    public class PL
    {
        public static bool tmp_screen = false;
        public static int tmp_calls = 0;

        public void askScreenshot(CSteamID steamID)
        {
            tmp_screen = true;
            tmp_calls += 1;
        }
    }
}
