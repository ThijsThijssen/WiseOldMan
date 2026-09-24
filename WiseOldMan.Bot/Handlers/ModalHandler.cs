using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using WiseOldMan.Bot.Constants;
using WiseOldMan.Bot.Data;
using WiseOldMan.Bot.Services;

namespace WiseOldMan.Bot.Handlers;

public class ModalHandler(PlayerService playerService)
{
    public async Task HandleAsync(SocketModal modal)
    {
        if (modal.Data.CustomId != "skill_modal")
            return;

        var selectedSkill = modal
            .Data.Components.FirstOrDefault(x => x.CustomId == "skill_select_menu")
            ?.Values.FirstOrDefault();

        var amountString = modal
            .Data.Components.FirstOrDefault(x => x.CustomId == "amount_input")
            ?.Value;

        if (selectedSkill is null || !int.TryParse(amountString, out var amount))
        {
            await modal.RespondAsync("Invalid input.");
            return;
        }

        var skillId = (int)Enum.Parse<Skills>(selectedSkill);

        await playerService.AddExperienceAsync(modal.User.Id, skillId, amount);

        await modal.RespondAsync(
            $"{modal.User.Mention} selected skill: {selectedSkill}, with amount: {amount}"
        );
    }
}
