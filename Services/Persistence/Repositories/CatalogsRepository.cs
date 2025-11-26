using Catalogs.Entities;
using Catalogs.Interfaces;
using Common.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class CatalogsRepository(AppDbContext context) : ICatalogsRepository
{
    public IUnitOfWork UnitOfWork => context;

    public void Add(Catalog aggregate)
    {
        context.Catalogs.Add(aggregate);
    }

    public void Remove(Catalog aggregate)
    {
        context.Catalogs.Remove(aggregate);
    }

    public async Task<Catalog?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await context.Catalogs.FirstOrDefaultAsync(c => c.Id == id, ct);
    }

    public async Task<ICollection<Catalog>> GetCollectionAsync(
        CancellationToken cancellationToken = default
    )
    {
        return await context.Catalogs.ToListAsync(cancellationToken);
    }
}
