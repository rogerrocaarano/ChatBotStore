using Common.Abstractions.Persistence;
using Customers.Entities;
using Customers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class CustomersRepository(AppDbContext context) : ICustomersRepository
{
    public IUnitOfWork UnitOfWork => context;

    public void Add(Customer aggregate)
    {
        context.Customers.Add(aggregate);
    }

    public void Remove(Customer aggregate)
    {
        context.Customers.Remove(aggregate);
    }



    public async Task<Customer?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await context
            .Customers.Include(c => c.Addresses)
            .FirstOrDefaultAsync(c => c.Id == id, ct);
    }

    public async Task<ICollection<Customer>> GetCollectionAsync(
        CancellationToken cancellationToken = default
    )
    {
        return await context
            .Customers.Include(customer => customer.Addresses)
            .ToListAsync(cancellationToken);
    }

    public Task<Customer?> GetByTelegramIdAsync(float telegramId, CancellationToken cancellationToken = default)
    {
        return context
            .Customers.Include(c => c.Addresses)
            .FirstOrDefaultAsync(c => c.Telegram.Id == telegramId, cancellationToken);
    }
}
