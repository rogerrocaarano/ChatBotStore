using Common.Abstractions.Domain;

namespace Common.ValueObjects;

public record PhoneNumber(string CountryCode, string Number) : IValueObject;
