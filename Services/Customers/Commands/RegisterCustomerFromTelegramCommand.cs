using Common.Types;
using LiteBus.Commands.Abstractions;

namespace Customers.Commands;

public record RegisterCustomerFromTelegramCommand(Telegram Telegram, string Name) : ICommand;
