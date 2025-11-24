using Common.Abstractions.Entities;

namespace Common.Types;

public record LocationPoint(float Latitude, float Longitude) : IValueObject;
