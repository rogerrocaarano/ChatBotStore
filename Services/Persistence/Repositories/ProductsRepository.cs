using Catalogs.Entities;
using Catalogs.Interfaces;
using Common.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class ProductsRepository(AppDbContext context) : IProductsRepository
{
    public IUnitOfWork UnitOfWork => context;

    public void Add(Product aggregate)
    {
        context.Products.Add(aggregate);
    }

    public void Remove(Product aggregate)
    {
        context.Products.Remove(aggregate);
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await context.Products.FirstOrDefaultAsync(p => p.Id == id, ct);
    }

    public async Task<ICollection<Product>> GetCollectionAsync(
        CancellationToken cancellationToken = default
    )
    {
        return await context.Products.ToListAsync(cancellationToken);
    }

    public async Task<ICollection<Product>> GetByCatalogIdAsync(
        Guid catalogId,
        CancellationToken cancellationToken = default
    )
    {
        return await context
            .Products.Where(p => p.CatalogId == catalogId)
            .ToListAsync(cancellationToken);
    }
}
