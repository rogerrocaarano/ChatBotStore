using System.Text.Json.Nodes;
using Common.Types;

namespace Common.Providers;

public interface ITelegramBotPort
{
    Task<IncomingChatMessage?> IncomingMessageAsync(JsonObject update);
    Task SendMessageAsync(Telegram to, string message);
    Task SendMessageWithOptionsAsync(
        Telegram to,
        string message,
        ICollection<BotOption> options,
        BotOption? mainOption = null,
        bool multiRow = false
    );
}
