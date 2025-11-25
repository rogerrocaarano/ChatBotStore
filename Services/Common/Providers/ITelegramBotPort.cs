using System.Text.Json.Nodes;
using Common.Types;

namespace Common.Providers;

public interface ITelegramBotPort
{
    Task SendMessageAsync(Telegram to, IncomingChatMessage message);
    IncomingChatMessage? ReceiveMessageAsync(JsonObject update);
}
