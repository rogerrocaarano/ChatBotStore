using Common.Abstractions.Entities;
using Common.Types;

namespace Catalogs.Entities;

public class Product : BaseEntity<Guid>, IAggregateRoot
{
    public Guid CatalogId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public Money? Price { get; private set; }
    public bool IsAvailable { get; private set; }

#pragma warning disable CS8618
    private Product() { } // For ORMs
#pragma warning restore CS8618

    private Product(Guid id, Guid catalogId, string name)
        : base(id)
    {
        CatalogId = catalogId;
        Name = name;
        IsAvailable = false; // Default
    }

    public static Product Create(Guid catalogId, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name is required.");

        return new Product(id: Guid.NewGuid(), catalogId: catalogId, name: name);
    }

    public Product WithDescription(string description)
    {
        Apply(() => Description = description);
        return this;
    }

    public Product PricedAt(Money price, Currency catalogCurrency)
    {
        // La regla de negocio se mantiene aquí
        if (price.Currency != catalogCurrency)
        {
            throw new InvalidOperationException($"Product currency must match Catalog currency.");
        }

        Apply(() => Price = price);
        return this;
    }

    public Product MarkAsAvailable()
    {
        if (Price == null)
        {
            throw new InvalidOperationException("Cannot mark as available without a price set.");
        }

        Apply(() => IsAvailable = true);
        return this;
    }

    public Product MarkAsUnavailable()
    {
        Apply(() => IsAvailable = false);
        return this;
    }
}
