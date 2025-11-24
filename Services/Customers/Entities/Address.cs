using Common.Abstractions.Entities;
using Common.Types;

namespace Customers.Entities;

public class Address : BaseEntity<Guid>
{
    public string? Name { get; private set; }
    public LocationPoint Location { get; private set; }

    public LocationDetails Details { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private Address() { } // For ORMs
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    private Address(Guid id, string? name, LocationPoint location, LocationDetails details)
        : base(id)
    {
        Name = name;
        Location = location;
        Details = details;
    }

    public static Address Create(LocationPoint location)
    {
        return new Address(Guid.NewGuid(), null, location, LocationDetails.WithDefaults);
    }

    public Address LabelAs(string name)
    {
        Apply(()=> Name = name);
        return this;
    }

    public Address SpecifyDetails(LocationDetails details)
    {
        Apply(()=> Details = details);
        return this;
    }

    public void CorrectCoordinates(LocationPoint location)
    {
        Apply(()=> Location = location);
    }
}
