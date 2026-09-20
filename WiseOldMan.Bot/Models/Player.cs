using System;
using System.Collections.Generic;
using System.Text;

namespace WiseOldMan.Bot.Models;

public class Player
{
    public int ID { get; set; }
    public string Name { get; set; }
    public ulong DiscordID { get; set; }

    public IList<PlayerSkill> Skills { get; set; }
    public IList<PlayerItem> Items { get; set; }
}
