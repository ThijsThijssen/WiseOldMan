using Discord;
using Discord.Interactions;
using Microsoft.EntityFrameworkCore;
using WiseOldMan.Bot.Constants;
using WiseOldMan.Bot.Data;
using WiseOldMan.Bot.Models;

namespace WiseOldMan.Bot.Modules;

[Group("player", "Player commands.")]
public class PlayerModule(DatabaseContext context) : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("create", "Creates a new player")]
    public async Task CreatePlayerAsync()
    {
        var discordId = Context.User.Id;
        var name = Context.User.Username;

        if (context.Players.Any(x => x.DiscordID == discordId))
        {
            await RespondAsync(
                text: "Player already exists for your Discord account",
                ephemeral: true
            );
        }

        var playerToCreate = new Player
        {
            Name = name,
            DiscordID = discordId,
            Skills =
            [
                new() { SkillID = Skills.Attack, Experience = 0 },
                new() { SkillID = Skills.Defence, Experience = 0 },
                new() { SkillID = Skills.Strength, Experience = 0 },
                new() { SkillID = Skills.Hitpoints, Experience = 0 },
                new() { SkillID = Skills.Ranged, Experience = 0 },
                new() { SkillID = Skills.Prayer, Experience = 0 },
                new() { SkillID = Skills.Magic, Experience = 0 },
                new() { SkillID = Skills.Cooking, Experience = 0 },
                new() { SkillID = Skills.Woodcutting, Experience = 0 },
                new() { SkillID = Skills.Fletching, Experience = 0 },
                new() { SkillID = Skills.Fishing, Experience = 0 },
                new() { SkillID = Skills.Firemaking, Experience = 0 },
                new() { SkillID = Skills.Crafting, Experience = 0 },
                new() { SkillID = Skills.Smithing, Experience = 0 },
                new() { SkillID = Skills.Mining, Experience = 0 },
                new() { SkillID = Skills.Herblore, Experience = 0 },
                new() { SkillID = Skills.Agility, Experience = 0 },
                new() { SkillID = Skills.Thieving, Experience = 0 },
                new() { SkillID = Skills.Slayer, Experience = 0 },
                new() { SkillID = Skills.Farming, Experience = 0 },
                new() { SkillID = Skills.Runecraft, Experience = 0 },
                new() { SkillID = Skills.Hunter, Experience = 0 },
                new() { SkillID = Skills.Construction, Experience = 0 },
            ],
        };

        try
        {
            var createdPlayer = context.Players.Add(playerToCreate);

            await context.SaveChangesAsync();

            await RespondAsync(text: $"Created player with name: {name}", ephemeral: true);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Something went wrong with creating player: {0}", ex.Message);
            await RespondAsync(text: $"Failed to create player with name: {name}", ephemeral: true);
        }
    }

    [SlashCommand("get", "Get player information")]
    public async Task GetPlayerAsync()
    {
        var discordId = Context.User.Id;

        var player = context
            .Players.Include(player => player.Skills)
                .ThenInclude(skill => skill.Skill)
            .FirstOrDefault(x => x.DiscordID == discordId);

        if (player == null)
        {
            await RespondAsync(
                text: "Player does not exists for your Discord account",
                ephemeral: true
            );
            return;
        }

        var totalExperience = player.Skills.Sum(x => x.Experience);

        var embed = new EmbedBuilder { Title = "Player information" };

        foreach (var skill in player.Skills)
        {
            embed.Fields.Add(
                new()
                {
                    Name = skill.Skill.Name,
                    Value = $"XP: {skill.Experience}",
                    IsInline = false,
                }
            );
        }

        await RespondAsync(embed: embed.Build());
    }
}
