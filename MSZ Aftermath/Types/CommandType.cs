using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;

namespace MSZ_Aftermath.Types
{
    public class CommandType
    {
        public string command;
        public bool hasNameDef;

        public CommandType(string command, bool hasNameDef)
        {
            this.command = command;
            this.hasNameDef = hasNameDef;
        }

        public bool isMe(string nick)
        {
            return Player.player.name.ToLower().Contains(nick.ToLower());
        }

        public virtual void execute(string[] args)
        {
        }
    }
}
