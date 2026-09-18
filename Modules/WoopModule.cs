using Discord.Interactions;

namespace WiseOldMan.Modules;

public class WoopModule : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("woop", "Pings the bot and returns its latency.")]
    public async Task WoopAsync() =>
        await RespondAsync(
            text: $":ping_pong: It took me {Context.Client.Latency}ms to respond to you!",
            ephemeral: true
        );

    [SlashCommand("skill", "Select a skill to get its details.")]
    public async Task SkillAsync(
        [Choice("Attack", "attack")]
        [Choice("Defence", "defence")]
        [Choice("Strength", "strength")]
        [Choice("Hitpoints", "hitpoints")]
        [Choice("Ranged", "ranged")]
        [Choice("Prayer", "prayer")]
        [Choice("Magic", "magic")]
        [Choice("Cooking", "cooking")]
        [Choice("Woodcutting", "woodcutting")]
        [Choice("Fletching", "fletching")]
        [Choice("Fishing", "fishing")]
        [Choice("Firemaking", "firemaking")]
        [Choice("Crafting", "crafting")]
        [Choice("Smithing", "smithing")]
        [Choice("Mining", "mining")]
        [Choice("Herblore", "herblore")]
        [Choice("Agility", "agility")]
        [Choice("Thieving", "thieving")]
        [Choice("Slayer", "slayer")]
        [Choice("Farming", "farming")]
        [Choice("Runecrafting", "runecrafting")]
        [Choice("Hunter", "hunter")]
        [Choice("Construction", "construction")] string skill
    )
    {
        await RespondAsync($"You selected the skill: {skill}");
    }
}
