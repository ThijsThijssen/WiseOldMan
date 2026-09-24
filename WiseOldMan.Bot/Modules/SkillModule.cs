using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Discord;
using Discord.Interactions;
using Microsoft.EntityFrameworkCore;
using WiseOldMan.Bot.Constants;
using WiseOldMan.Bot.Data;
using WiseOldMan.Bot.Enums;
using WiseOldMan.Bot.Models;

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

    [SlashCommand("modal", "Show a modal of the skill you want to train.")]
    public async Task SkillModalAsync()
    {
        var options = Enum.GetValues<Skills>()
            .Select(skill => new SelectMenuOptionBuilder()
            {
                Label = skill.ToString(),
                Value = skill.ToString(),
            })
            .ToList();

        var selectMenuBuilder = new SelectMenuBuilder() { IsRequired = true }
            .WithCustomId("skill_select_menu")
            .WithOptions(options);

        var modalBuilder = new ModalBuilder()
            .WithTitle("Skill Modal")
            .WithCustomId("skill_modal")
            .AddSelectMenu("Skill", selectMenuBuilder)
            .AddTextInput(
                "Amount",
                "amount_input",
                TextInputStyle.Short,
                "Enter a number...",
                required: true
            );

        await Context.Interaction.RespondWithModalAsync(modalBuilder.Build());
    }
}
