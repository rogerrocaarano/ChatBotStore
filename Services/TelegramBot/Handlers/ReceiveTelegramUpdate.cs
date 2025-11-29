using System.Text.Json;
using Common.Providers;
using Common.Types;
using Customers.Commands;
using LiteBus.Commands.Abstractions;
using LiteBus.Events.Abstractions;
using TelegramBot.Commands;
using TelegramBot.Types;

namespace TelegramBot.Handlers;

public class ReceiveTelegramUpdate(
    ITelegramBotPort telegramBot,
    IEventPublisher eventPublisher,
    ICommandMediator commandMediator
) : ICommandHandler<ReceiveTelegramUpdateCommand>
{
    public async Task HandleAsync(
        ReceiveTelegramUpdateCommand message,
        CancellationToken cancellationToken = new CancellationToken()
    )
    {
        var telegramUpdate = message.Update.Deserialize<Update>(
            TelegramBotModule.SerializerOptions
        );

        // Hacemos switch sobre una Tupla: (Texto, UserId)
        ICommand? command = (telegramUpdate?.Message?.Text, telegramUpdate?.Message?.From.Id) switch
        {
            ("/start", { } userId) => HandleStartCommand(telegramUpdate.Message.From),

            _ => null,
        };
        if (command is null)
        {
            Console.WriteLine("Invalid command received from Telegram.");
            return;
        }

        await commandMediator.SendAsync(command, cancellationToken);
    }

    private static RegisterCustomerFromTelegramCommand HandleStartCommand(User user)
    {
        var name = user.FirstName.Trim().ToUpper();
        var fullName = !string.IsNullOrEmpty(name) ? name : "NN";

        var lastName = user.LastName?.Trim().ToUpper();
        if (!string.IsNullOrEmpty(lastName))
        {
            fullName += " " + lastName;
        }

        var telegram = new Telegram(user.Id);

        return new RegisterCustomerFromTelegramCommand(telegram, fullName);
    }
}
