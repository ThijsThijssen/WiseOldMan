using System;
using System.Collections.Generic;
using System.Text;

namespace WiseOldMan.Bot.Models;

public class PlayerSkill
{
    public int PlayerSkillID { get; set; }
    public int SkillID { get; set; }
    public ulong PlayerID { get; set; }
    public int Experience { get; set; }

    public Skill Skill { get; set; }
    public Player Player { get; set; }
}
