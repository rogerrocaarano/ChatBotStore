using Common.Abstractions.Entities;

namespace Common.Types;

public record Currency(string Code, string Symbol) : IValueObject
{
    public static readonly Currency USD = new("USD", "$");
    public static readonly Currency EUR = new("EUR", "€");
    public static readonly Currency GBP = new("GBP", "£");
    public static readonly Currency BOB = new("BOB", "Bs.");
}
