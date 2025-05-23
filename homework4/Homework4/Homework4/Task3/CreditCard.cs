namespace Homework4.Task3;

public class CreditCard
{
    public double Balance { get; set; }
    public string CVCcode { get; init; }

    public CreditCard(double balance, string cvccode)
    {
        Balance = balance;
        CVCcode = cvccode;
    }

    // operator overload +
    public static CreditCard operator +(CreditCard card, double sum)
    {
        card.Balance += sum;
        return card;
    }

    // operator overload -
    public static CreditCard operator -(CreditCard card, double sum)
    {
        card.Balance -= sum;
        return card;
    }

    // operator overload ==
    public static bool operator ==(CreditCard card1, CreditCard card2)
    {
        return card1.CVCcode == card2.CVCcode;
    }

    // operator overload !=
    public static bool operator !=(CreditCard card1, CreditCard card2)
    {
        return card1.CVCcode != card2.CVCcode;
    }

    // operator overload >
    public static bool operator >(CreditCard card1, CreditCard card2)
    {
        return card1.Balance > card2.Balance;
    }

    // operator overload <
    public static bool operator <(CreditCard card1, CreditCard card2)
    {
        return card1.Balance < card2.Balance;
    }

    //overload for methods
    public override bool Equals(object? obj)
    {
        if (obj is CreditCard otherCard) return CVCcode == otherCard.CVCcode;
        return false;
    }

    public override int GetHashCode() => CVCcode.GetHashCode();
    public override string ToString() => $"Balance: {Balance}, CVCcode: {CVCcode}";
}