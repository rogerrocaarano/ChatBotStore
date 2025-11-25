using Common.Abstractions.Entities;

namespace Common.Types;

public record IncomingChatMessage(string From, string Text) : IValueObject;
