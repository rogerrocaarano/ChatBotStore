using Customers.Commands;
using Customers.Interfaces;
using Customers.Queries;
using LiteBus.Commands.Abstractions;
using LiteBus.Messaging.Abstractions;
using LiteBus.Queries.Abstractions;

namespace Customers.PreHandlers;

public class ValidateTelegramCustomerExists(IQueryMediator queryMediator)
    : ICommandPreHandler<RegisterCustomerFromTelegramCommand>
{
    public async Task PreHandleAsync(
        RegisterCustomerFromTelegramCommand message,
        CancellationToken cancellationToken = default
    )
    {
        var query = new GetCustomerIdFromTelegramQuery(message.Telegram);
        var customerId = await queryMediator.QueryAsync(query, cancellationToken);

        if (customerId != Guid.Empty)
        {
            Console.WriteLine("Customer already registered: " + customerId);

            AmbientExecutionContext.Current.Abort();
        }
    }
}
