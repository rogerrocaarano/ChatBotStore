using Common.Abstractions.Entities;

namespace Common.Types;

public record Telegram(long Id) : IValueObject;
