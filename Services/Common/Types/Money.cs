using Common.Abstractions.Entities;

namespace Common.Types;

public record Money(decimal Amount, Currency Currency) : IValueObject
{
    public static Money operator +(Money left, Money right)
    {
        CheckCurrencyMatch(left, right);
        return new Money(left.Amount + right.Amount, left.Currency);
    }

    public static Money operator -(Money left, Money right)
    {
        CheckCurrencyMatch(left, right);
        return new Money(left.Amount - right.Amount, left.Currency);
    }

    public static Money operator *(Money left, decimal multiplier)
    {
        return new Money(left.Amount * multiplier, left.Currency);
    }

    public static Money operator *(decimal multiplier, Money right)
    {
        return new Money(right.Amount * multiplier, right.Currency);
    }

    public static Money operator /(Money left, decimal divisor)
    {
        if (divisor == 0)
        {
            throw new DivideByZeroException("Cannot divide Money by zero.");
        }
        return new Money(left.Amount / divisor, left.Currency);
    }

    private static void CheckCurrencyMatch(Money left, Money right)
    {
        if (left.Currency != right.Currency)
        {
            throw new InvalidOperationException($"Currency mismatch: Cannot operate {left.Currency} with {right.Currency}. Convert first.");
        }
    }

    public static Money operator -(Money value)
    {
        return new Money(-value.Amount, value.Currency);
    }
}
