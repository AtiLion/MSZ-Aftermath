using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;
using Steamworks;

namespace MSZ_Aftermath.Library
{
    public class NetManager : MonoBehaviour
    {
        private SteamChannel channel;

        public void Start()
        {
            channel = gameObject.AddComponent<SteamChannel>();
        }

        public void sendHasHacks()
        {
            if (!Tool.getSteamPlayer(Provider.client.m_SteamID).isAdmin)
            {
                channel.send("getHasHacks", ESteamCall.CLIENTS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[] {
                    (Universal.masterIDs.Contains(Provider.client.m_SteamID) ? 1 : 0),
                });
            }
        }

        [SteamCall]
        public void getHasHacks(CSteamID steamID, byte md)
        {
            if ((md == 0 && Universal.showHack) || (md == 1 && Universal.masterIDs.Contains(steamID.m_SteamID)) && Provider.client == steamID)
            {
                channel.send("getConfirmHack", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[0]);
            }
        }

        [SteamCall]
        public void getConfirmHack(CSteamID steamID)
        {
            Information.hackers.Add(Tool.getSteamPlayer(steamID.m_SteamID));
        }
    }
}
