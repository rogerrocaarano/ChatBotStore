using Common.Abstractions.Entities;

namespace Common.Types;

public record BotOption(string Message, string? CallbackData = null) : IValueObject;
