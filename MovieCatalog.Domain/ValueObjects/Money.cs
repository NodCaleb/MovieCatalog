namespace MovieCatalog.Domain.ValueObjects;

/// <summary>
/// Value object to represent money (used for budget)
/// </summary>
public class Money
{
    public decimal Amount { get; private set; }
    public string Currency { get; private set; }

    public Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }
}
