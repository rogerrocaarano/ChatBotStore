using Common.Abstractions.Domain;
using Common.ValueObjects;

namespace Customers.Entities;

public class Customer : BaseEntity<Guid>, IAggregateRoot
{
    public string Name { get; private set; }
    public PhoneNumber? PhoneNumber { get; private set; }
    public Telegram? Telegram { get; private set; }
    public IReadOnlyCollection<Address> Addresses => _addresses.AsReadOnly();

    private readonly List<Address> _addresses = [];

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private Customer() { } // For ORMs
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    private Customer(
        Guid id,
        string name,
        PhoneNumber? phoneNumber,
        Telegram? telegram,
        List<Address> addresses
    )
        : base(id)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Telegram = telegram;
        _addresses = addresses;
    }

    public static Customer Create(string name)
    {
        return new Customer(
            id: Guid.NewGuid(),
            name: name,
            phoneNumber: null,
            telegram: null,
            addresses: []
        );
    }

    public Customer AcceptsCallsAt(PhoneNumber phoneNumber)
    {
        Apply(()=> PhoneNumber = phoneNumber);
        return this;
    }

    public Customer LinkWithTelegram(Telegram telegram)
    {
        Apply(()=> Telegram = telegram);
        return this;
    }

    public void RegisterAddress(Address address)
    {
        Apply(()=> _addresses.Add(address));
    }

    public void ForgetAddress(Address address)
    {
        var existing = _addresses.FirstOrDefault(addr => addr.Id == address.Id);
        if (existing != null)
        {
            Apply(()=> _addresses.Remove(address));
        }
    }
}
