namespace Homework2.OOP1;

public class Product : Money
{
    public string Name { get;}
        
    public Product(string name, int dollars, int cents) : base(dollars, cents)
    {
        Name = name;
    }
    public void ReducePrice(int reduceDollars, int reduceCents)
    {
        // convert cents to dollars
        int totalCents = this.dollars * 100 + this.cents;
        int reduceAmount = reduceDollars * 100 + reduceCents;

        totalCents -= reduceAmount;

        if (totalCents < 0)
            totalCents = 0; // the price cannot be negative

        this.dollars = totalCents / 100;
        this.cents = totalCents % 100;

        Console.WriteLine($"Price of {Name} reduced!");
    }
}