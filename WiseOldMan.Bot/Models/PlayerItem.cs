using System;
using System.Collections.Generic;
using System.Text;

namespace WiseOldMan.Bot.Models;

public class PlayerItem
{
    public int PlayerItemID { get; set; }
    public int ItemID { get; set; }
    public ulong PlayerID { get; set; }
    public int Amount { get; set; }

    public Item Item { get; set; }
    public Player Player { get; set; }
}
