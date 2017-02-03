using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;
using MSZ_Aftermath.Types;

namespace MSZ_Aftermath.Commands
{
    public class CMD_Vac : CommandType
    {
        public CMD_Vac() : base("!vac", true) {}

        public override void execute(string[] args)
        {
            if (args[0] == "" || isMe(args[0]))
            {
                Provider._connectionFailureInfo = ESteamConnectionFailureInfo.AUTH_VAC_BAN;
                Provider.disconnect();
            }
        }
    }
}
