using Common.Types;

namespace Common.Providers;

public interface ITelegramPort
{
    Task SendMessageAsync(Telegram to, ChatMessage message);
}
