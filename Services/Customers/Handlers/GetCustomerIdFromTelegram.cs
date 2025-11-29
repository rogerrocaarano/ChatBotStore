using Customers.Interfaces;
using Customers.Queries;
using LiteBus.Queries.Abstractions;

namespace Customers.Handlers;

public class GetCustomerIdFromTelegram(ICustomersRepository storedCustomers)
    : IQueryHandler<GetCustomerIdFromTelegramQuery, Guid>
{
    public async Task<Guid> HandleAsync(
        GetCustomerIdFromTelegramQuery message,
        CancellationToken cancellationToken = default
    )
    {
        var customer = await storedCustomers.GetByTelegramIdAsync(
            message.Telegram.Id,
            cancellationToken
        );

        return customer?.Id ?? Guid.Empty;
    }
}
