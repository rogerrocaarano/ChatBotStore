using Common.Providers;
using Common.Types;
using RestSharp;

namespace Telegram;

public class TelegramAdapter(RestClient apiClient) : ITelegramPort
{
    public Task SendMessageAsync(Common.Types.Telegram to, ChatMessage message)
    {
        throw new NotImplementedException();
    }
}
