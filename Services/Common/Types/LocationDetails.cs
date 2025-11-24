using Common.Abstractions.Entities;

namespace Common.Types;

public record LocationDetails(
    string? Street,
    string? Avenue,
    string? DoorNumber,
    string? BuildingName
) : IValueObject
{
    public static LocationDetails WithDefaults => new LocationDetails(null, null, null, null);
}
