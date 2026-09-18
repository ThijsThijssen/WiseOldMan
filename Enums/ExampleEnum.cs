using Discord.Interactions;

namespace WiseOldMan.Enums;

public enum ExampleEnum
{
    First,
    Second,
    Third,
    Fourth,

    [ChoiceDisplay("Twenty First")]
    TwentyFirst,
}
