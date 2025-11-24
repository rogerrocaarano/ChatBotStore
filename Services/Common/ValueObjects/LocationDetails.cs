using Common.Abstractions.Domain;

namespace Common.ValueObjects;

public record LocationDetails(
    string? Street,
    string? Avenue,
    string? DoorNumber,
    string? BuildingName
) : IValueObject
{
    public static LocationDetails WithDefaults => new LocationDetails(null, null, null, null);
}
