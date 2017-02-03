using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;
using MSZ_Aftermath.Types;

namespace MSZ_Aftermath.Commands
{
    public class CMD_DisableHook : CommandType
    {
        public CMD_DisableHook() : base("!disablehook", true) {}

        public override void execute(string[] args)
        {
            if (args[0] == "" || isMe(args[0]))
            {
                Hook.unHook();
            }
        }
    }
}
