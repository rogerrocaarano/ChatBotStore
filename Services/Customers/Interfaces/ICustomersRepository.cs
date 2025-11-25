using Common.Abstractions.Persistence;
using Customers.Entities;

namespace Customers.Interfaces;

public interface ICustomersRepository: IRepository<Customer>
{
    Task<Customer?> GetByTelegramIdAsync(
        float telegramId,
        CancellationToken cancellationToken = default
    );
}
