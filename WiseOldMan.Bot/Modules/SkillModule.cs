using System;
using System.Collections.Generic;
using System.Text;
using Discord.Interactions;
using Microsoft.EntityFrameworkCore;
using WiseOldMan.Bot.Constants;
using WiseOldMan.Bot.Data;
using WiseOldMan.Bot.Enums;

namespace WiseOldMan.Bot.Modules;

[Group("skill", "Skill commands.")]
public class SkillModule(DatabaseContext context) : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("get", "Get information about the selected Skill.")]
    public async Task GetSkillAsync(Skills name)
    {
        var discordId = Context.User.Id;
        var skillId = (int)name;

        var player = await context
            .Players.Include(x => x.Skills)
                .ThenInclude(x => x.Skill)
            .FirstOrDefaultAsync(x => x.DiscordID == discordId);
        var playerSkill = player.Skills.FirstOrDefault(x => x.SkillID == skillId);

        await RespondAsync(
            $"{Context.User.Mention}'s {playerSkill.Skill.Name} experience: {playerSkill.Experience}"
        );
    }

    [SlashCommand("train", "Train the selected Skill.")]
    public async Task TrainSkillAsync(Skills name)
    {
        var discordId = Context.User.Id;
        var skillId = (int)name;

        var player = await context
            .Players.Include(x => x.Skills)
                .ThenInclude(x => x.Skill)
            .FirstOrDefaultAsync(x => x.DiscordID == discordId);
        var playerSkill = player.Skills.FirstOrDefault(x => x.SkillID == skillId);

        playerSkill.Experience += 1000;

        await context.SaveChangesAsync();

        await RespondAsync(
            $"{Context.User.Mention}'s {playerSkill.Skill.Name} experience is increased to: {playerSkill.Experience}"
        );
    }
}
