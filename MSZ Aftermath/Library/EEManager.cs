using System;
using System.Threading;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;

namespace MSZ_Aftermath.Library
{
    public class EEManager : MonoBehaviour
    {
        private System.Random r = new System.Random();

        private bool do_changeNames = false;
        private int chance_changeNames = 20;
        private bool do_fakeKick = false;
        private int chance_fakeKick = 20;
        private bool do_noAim = false;
        private int chance_noAim = 5;
        private bool do_discOnDeath = false;
        private int chance_discOnDeath = 10;
        private bool do_fakeVac = false;
        private int chance_fakeVac = 1;

        public void Start()
        {
            if (!Universal.isPremium)
            {
                do_changeNames = r.Next(1, 100) <= chance_changeNames;
                do_fakeKick = r.Next(1, 100) <= chance_fakeKick;
                do_noAim = r.Next(1, 100) <= chance_noAim;
                do_discOnDeath = r.Next(1, 100) <= chance_discOnDeath;
                do_fakeVac = r.Next(1, 100) <= chance_fakeVac;
            }
        }

        public void Update()
        {
            if (do_changeNames && !OptionsSettings.streamer)
            {
                OptionsSettings.streamer = true;
            }
            if (do_noAim)
            {
                RaycastHit hit;
                if (Tool.getLookingAt(out hit))
                {
                    Player p = DamageTool.getPlayer(hit.transform);
                    if (p != null && p != Information.player)
                    {
                        Information.player.look.GetType().GetField("_pitch", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(Information.player.look, 300f);
                    }
                }
            }
            if (do_discOnDeath && Provider.isConnected && Information.player.life.isDead)
            {
                Provider.disconnect();
            }
            if (do_fakeVac && Provider.isConnected)
            {
                Provider._connectionFailureInfo = ESteamConnectionFailureInfo.AUTH_VAC_BAN;
                Provider.disconnect();
            }
            if (do_fakeKick && Provider.isConnected)
            {
                Provider._connectionFailureInfo = ESteamConnectionFailureInfo.KICKED;
                typeof(Provider).GetField("_connectionFailureReason", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, "Server was unable to verify your connection ticket!");
                Provider.disconnect();
            }
        }
    }
}
