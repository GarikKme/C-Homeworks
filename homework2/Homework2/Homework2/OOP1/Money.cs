namespace Homework2.OOP1;

public class Money : ISetMoney
{
    protected int dollars;
    protected int cents;

    public Money(int dollars, int cents)
    {
        SetMoney(dollars, cents);
    }

    public void ShowMoney()
    {
        Console.WriteLine($"Money in a bag: {this.dollars} $, {this.cents} cents ");
    }

    public void SetMoney(int dollars, int cents)
    {
        this.dollars = dollars + cents / 100;
        this.cents = cents % 100;
    }
}