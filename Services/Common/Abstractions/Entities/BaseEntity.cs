using Common.Abstractions.Events;

namespace Common.Abstractions.Entities;

public abstract class BaseEntity<TId>
{
    public TId Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime UpdatedAt { get; protected set; }
    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    public bool Deleted { get; private set; }

    protected BaseEntity(TId id)
    {
        Id = id;
        var timeNow = DateTime.UtcNow;
        CreatedAt = timeNow;
        UpdatedAt = timeNow;
        Deleted = false;
    }

    // For ORM frameworks (like EF Core) that may require a parameterless constructor
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    protected BaseEntity() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    protected void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void Apply(Action action, IDomainEvent? domainEvent = null)
    {
        action();
        UpdatedAt = DateTime.UtcNow;

        if (domainEvent != null)
        {
            _domainEvents.Add(domainEvent);
        }
    }

    public override bool Equals(object? obj)
    {
        if (obj is not BaseEntity<TId> other)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (GetType() != other.GetType())
            return false;

        if (EqualityComparer<TId>.Default.Equals(Id, default))
            return false;

        return EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<TId>.Default.Equals(Id, default)
            ? base.GetHashCode()
            : Id!.GetHashCode();
    }

    public static bool operator ==(BaseEntity<TId>? a, BaseEntity<TId>? b)
    {
        if (a is null && b is null)
            return true;
        if (a is null || b is null)
            return false;
        return a.Equals(b);
    }

    public static bool operator !=(BaseEntity<TId>? a, BaseEntity<TId>? b)
    {
        return !(a == b);
    }

    private void MarkAsDeleted(IDomainEvent? domainEvent = null)
    {
        Apply(() => Deleted = true, domainEvent);
    }
}
