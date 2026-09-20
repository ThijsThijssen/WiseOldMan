using System;
using System.Collections.Generic;
using System.Text;
using WiseOldMan.Bot.Models;

namespace WiseOldMan.Bot.Data;

public static class DbInitializer
{
    public static void Initialize(DatabaseContext context)
    {
        if (context.Skills.Any())
        {
            return; // DB already has been seeded with Skills
        }

        var skills = new Skill[]
        {
            new() { Name = "Attack" },
            new() { Name = "Defence" },
            new() { Name = "Strength" },
            new() { Name = "Hitpoints" },
            new() { Name = "Ranged" },
            new() { Name = "Prayer" },
            new() { Name = "Magic" },
            new() { Name = "Cooking" },
            new() { Name = "Woodcutting" },
            new() { Name = "Fletching" },
            new() { Name = "Fishing" },
            new() { Name = "Firemaking" },
            new() { Name = "Crafting" },
            new() { Name = "Smithing" },
            new() { Name = "Mining" },
            new() { Name = "Herblore" },
            new() { Name = "Agility" },
            new() { Name = "Thieving" },
            new() { Name = "Slayer" },
            new() { Name = "Farming" },
            new() { Name = "Runecraft" },
            new() { Name = "Hunter" },
            new() { Name = "Construction" },
        };

        context.Skills.AddRange(skills);
        context.SaveChanges();
    }
}
