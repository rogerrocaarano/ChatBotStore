using Common.Abstractions.Domain;

namespace Common.ValueObjects;

public record LocationPoint(float Latitude, float Longitude) : IValueObject;
