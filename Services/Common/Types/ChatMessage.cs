using Common.Abstractions.Entities;

namespace Common.Types;

public record ChatMessage(string MessageText, DateTime SentAt) : IValueObject;
