namespace TelegramBot.Types;

internal record class User(int Id, bool IsBot, string FirstName, string? LastName, string? Username);
