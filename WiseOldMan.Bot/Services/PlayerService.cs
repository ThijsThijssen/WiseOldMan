using Microsoft.EntityFrameworkCore;
using WiseOldMan.Bot.Data;

namespace WiseOldMan.Bot.Services;

public class PlayerService(IDbContextFactory<DatabaseContext> contextFactory)
{
    public async Task AddExperienceAsync(ulong discordId, int skillId, int amount)
    {
        await using var context = await contextFactory.CreateDbContextAsync();

        var player = await context
            .Players.Include(x => x.Skills)
            .FirstOrDefaultAsync(x => x.DiscordID == discordId);

        if (player is null)
        {
            return;
        }

        var playerSkill = player.Skills.FirstOrDefault(x => x.SkillID == skillId);

        if (playerSkill is null)
        {
            return;
        }

        playerSkill.Experience += amount;

        await context.SaveChangesAsync();
    }
}
