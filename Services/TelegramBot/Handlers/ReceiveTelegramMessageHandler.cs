using Common.Providers;
using LiteBus.Commands.Abstractions;
using TelegramBot.Commands;

namespace TelegramBot.Handlers;

public class ReceiveTelegramMessageHandler(ITelegramBotPort telegramBot) : ICommandHandler<ReceiveTelegramMessageCommand>
{
    public Task HandleAsync(ReceiveTelegramMessageCommand telegramMessage, CancellationToken cancellationToken = new CancellationToken())
    {
        var incomingMessage = telegramBot.ReceiveMessageAsync(telegramMessage.Update);
        if (incomingMessage is not null)
        {
            // Process the incoming message as needed
            Console.WriteLine($"Received message: {incomingMessage.Text} from {incomingMessage.From}");
        }
        return Task.CompletedTask;
    }
}
