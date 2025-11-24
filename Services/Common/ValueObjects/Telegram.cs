using Common.Abstractions.Domain;

namespace Common.ValueObjects;

public record Telegram(long Id) : IValueObject;
