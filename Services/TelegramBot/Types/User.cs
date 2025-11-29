namespace TelegramBot.Types;

internal record class User(
    long Id,
    bool IsBot,
    string FirstName,
    string? LastName,
    string? Username
);
