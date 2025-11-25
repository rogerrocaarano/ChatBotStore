namespace TelegramBot.Types;

internal record class Update(int UpdateId, Message? Message, Message? EditedMessage);
