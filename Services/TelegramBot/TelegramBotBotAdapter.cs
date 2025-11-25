using System.Text.Json;
using System.Text.Json.Nodes;
using Common.Providers;
using Common.Types;
using RestSharp;
using TelegramBot.Types;

namespace TelegramBot;

public class TelegramBotBotAdapter(RestClient apiClient) : ITelegramBotPort
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        PropertyNameCaseInsensitive = true
    };

    public Task SendMessageAsync(Telegram to, IncomingChatMessage message)
    {
        throw new NotImplementedException();
    }

    public IncomingChatMessage? ReceiveMessageAsync(JsonObject update)
    {
        try
        {
            var telegramUpdate = update.Deserialize<Update>(JsonOptions);
            if (telegramUpdate?.Message?.Text is null)
            {
                return null;
            }

            var chatMessageContent = new IncomingChatMessage(
                From: telegramUpdate.Message.From.ToString(),
                Text: telegramUpdate.Message.Text
            );

            return chatMessageContent;
        }
        catch (JsonException)
        {
            return null;
        }
    }
}
