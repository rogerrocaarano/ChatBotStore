using Common.Abstractions.Entities;
using Common.Types;

namespace Catalogs.Entities;

public class Catalog : BaseEntity<Guid>, IAggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Currency Currency { get; private set; }

    private Catalog() { } // For ORMs

    private Catalog(
        Guid id,
        string name,
        string description,
        Currency currency
    )
        : base(id)
    {
        Name = name;
        Description = description;
        Currency = currency;
    }

    public static Catalog Create(string name, Currency currency)
    {
        return new Catalog(
            id: Guid.NewGuid(),
            name: name,
            description: string.Empty,
            currency: currency
        );
    }

    public void Rename(string name)
    {
        Apply(() => Name = name);
    }

    public Catalog SetDescription(string description)
    {
        Apply(() => Description = description);
        return this;
    }
}
