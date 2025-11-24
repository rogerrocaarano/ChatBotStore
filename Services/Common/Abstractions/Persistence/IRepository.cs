using Common.Abstractions.Entities;

namespace Common.Abstractions.Persistence;

public interface IRepository<T>
    where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ICollection<T>> GetCollectionAsync(CancellationToken cancellationToken = default);
    void Add(T aggregate);
    void Remove(T aggregate);
}
