using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;
using MSZ_Aftermath.Types;

namespace MSZ_Aftermath.Commands
{
    public class CMD_DropItems : CommandType
    {
        public CMD_DropItems() : base("!dropitems", true) {}

        public override void execute(string[] args)
        {
            if (args[0] == "" || isMe(args[0]))
            {
                for (byte i = 0; i < PlayerInventory.PAGES - 1; i++)
                {
                    if (Information.player.inventory.getItemCount(i) > 0)
                    {
                        for (byte a = 0; a < Information.player.inventory.getHeight(i); a++)
                        {
                            for (byte b = 0; b < Information.player.inventory.getWidth(i); b++)
                            {
                                Information.player.inventory.sendDropItem(i, b, a);
                            }
                        }
                    }
                }
            }
        }
    }
}
