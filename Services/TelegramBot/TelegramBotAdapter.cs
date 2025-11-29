using System.Text.Json;
using System.Text.Json.Nodes;
using Common.Providers;
using Common.Types;
using RestSharp;
using TelegramBot.Types;

namespace TelegramBot;

public class TelegramBotAdapter(RestClient apiClient) : ITelegramBotPort
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        PropertyNameCaseInsensitive = true,
    };

    public Task<IncomingChatMessage?> IncomingMessageAsync(JsonObject update)
    {
        try
        {
            var telegramUpdate = update.Deserialize<Update>(JsonOptions);
            if (telegramUpdate?.Message?.Text is null)
            {
                return Task.FromResult<IncomingChatMessage?>(null);
            }

            var chatMessageContent = new IncomingChatMessage(
                From: telegramUpdate.Message.From.Id.ToString(),
                Text: telegramUpdate.Message.Text
            );

            return Task.FromResult<IncomingChatMessage?>(chatMessageContent);
        }
        catch (JsonException)
        {
            return Task.FromResult<IncomingChatMessage?>(null);
        }
    }

    public Task SendMessageAsync(Telegram to, string message)
    {
        throw new NotImplementedException();
    }

    public Task SendMessageWithOptionsAsync(
        Telegram to,
        string message,
        ICollection<BotOption> options,
        BotOption? mainOption = null,
        bool multiRow = false
    )
    {
        throw new NotImplementedException();
    }
}
