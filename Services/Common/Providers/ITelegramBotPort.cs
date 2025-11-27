using System.Text.Json.Nodes;
using Common.Types;

namespace Common.Providers;

public interface ITelegramBotPort
{
    Task SendMessageAsync(Telegram to, string message);
    Task ReplyToMessageAsync(IncomingChatMessage update);
    Task<IncomingChatMessage?> IncommingMessageAsync(JsonObject update);
}
