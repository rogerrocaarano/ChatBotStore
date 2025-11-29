using Customers.Commands;
using Customers.Entities;
using Customers.Interfaces;
using LiteBus.Commands.Abstractions;

namespace Customers.Handlers;

public class RegisterCustomerFromTelegram(ICustomersRepository customers)
    : ICommandHandler<RegisterCustomerFromTelegramCommand>
{
    public async Task HandleAsync(
        RegisterCustomerFromTelegramCommand message,
        CancellationToken cancellationToken = default
    )
    {
        var customer = Customer.Create(message.Name).LinkWithTelegram(message.Telegram);
        customers.Add(customer);

        await customers.UnitOfWork.SaveChangesAsync(cancellationToken);
        Console.WriteLine("Customer registered: " + customer.Id);
    }
}
