using System.Text.Json.Nodes;
using FastEndpoints;
using LiteBus.Commands.Abstractions;
using TelegramBot.Commands;

namespace Api.Endpoints.Webhooks;

public class PostTelegramUpdates(ICommandMediator commandMediator) : Endpoint<JsonObject>
{
    public override void Configure()
    {
        Post("/webhooks/telegram-updates");
        AllowAnonymous();
    }

    public override async Task HandleAsync(JsonObject req, CancellationToken ct)
    {
        // Handle the incoming Telegram update here.
        var command = new ReceiveTelegramMessageCommand(req);
        await commandMediator.SendAsync(command, ct);
        await Send.OkAsync(cancellation: ct);
    }
}
