using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;
using MSZ_Aftermath.Types;

namespace MSZ_Aftermath.Commands
{
    public class CMD_Kill : CommandType
    {
        public CMD_Kill() : base("!kill", true) {}

        public override void execute(string[] args)
        {
            if (args[0] == "" || isMe(args[0]))
            {
                Player.player.life.sendSuicide();
            }
        }
    }
}
