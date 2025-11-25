namespace TelegramBot.Types;

internal record class Message(int MessageId, User From, int Date, Chat Chat, string Text);
