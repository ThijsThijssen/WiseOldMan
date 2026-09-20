using Discord.Interactions;

namespace WiseOldMan.Bot.Enums;

public enum ExampleEnum
{
    First,
    Second,
    Third,
    Fourth,

    [ChoiceDisplay("Twenty First")]
    TwentyFirst,
}
