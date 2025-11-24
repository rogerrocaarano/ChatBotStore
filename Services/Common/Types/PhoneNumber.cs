using Common.Abstractions.Entities;

namespace Common.Types;

public record PhoneNumber(string CountryCode, string Number) : IValueObject;
