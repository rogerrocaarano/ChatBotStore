using Catalogs.Entities;
using Common.Abstractions.Persistence;

namespace Catalogs.Interfaces;

public interface IProductsRepository : IRepository<Product>
{
    Task<ICollection<Product>> GetByCatalogIdAsync(
        Guid catalogId,
        CancellationToken cancellationToken = default
    );
}
