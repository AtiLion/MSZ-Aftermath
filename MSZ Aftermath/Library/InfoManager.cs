using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;

namespace MSZ_Aftermath.Library
{
    public class InfoManager : MonoBehaviour
    {
        private DateTime update_lPlayer;

        public void Start()
        {

        }

        public void Update()
        {
            if (Universal.inGame)
            {
                if (update_lPlayer == null || (DateTime.Now - update_lPlayer).TotalMilliseconds >= 2000)
                {
                    Information.player = Player.player;
                    update_lPlayer = DateTime.Now;
                }

                if (Information.player != null && Information.player.input != null)
                {
                    Information.trustClient = Information.player.input.recov == -1;
                }
            }
        }

        public void OnGUI()
        {

        }
    }
}
