using Homework4.Task1;
using Homework4.Task2;
using Homework4.Task3;

namespace Homework4;

class Program
{
    static void Main(string[] args)
    {
        //Task 1
        Employee frontender = new Employee("Petya", "React Developer", 3000);
        Employee backender = new Employee("Vasya", "C# Developer", 3500);
        Console.WriteLine(frontender);
        Console.WriteLine(backender);
        frontender.Salary += 500;
        backender.Salary += 1500;
        Console.WriteLine("After changes");
        Console.WriteLine(frontender);
        Console.WriteLine(backender);

        Console.WriteLine(frontender == backender ? "Equal" : "Not equal");
        Console.WriteLine(frontender < backender ? "Frontender salary is smaller than Backender" : "Backender salary is smaller than Frontender");

        //Task 2
        City Harkiv = new City("Harkiv", 500000);
        City Lviv = new City("Lviv", 800000);
        Console.WriteLine(Harkiv);
        Console.WriteLine(Lviv);

        Harkiv = Harkiv + 150000;
        Lviv = Lviv + 100000;
        Console.WriteLine(Harkiv);
        Console.WriteLine(Lviv);

        Console.WriteLine(Harkiv == Lviv ? "Equal" : "Not equal");
        Console.WriteLine(Harkiv > Lviv ? "Harkiv has bigger population" : "Lviv has bigger population");


        //Task 3
        CreditCard creditCard = new CreditCard(1000, "232");
        CreditCard creditCard2 = new CreditCard(1500, "233");
        Console.WriteLine(creditCard);
        Console.WriteLine(creditCard2);

        creditCard = creditCard + 4500;
        creditCard2 = creditCard2 + 3500;
        
        Console.WriteLine(creditCard);
        Console.WriteLine(creditCard2);

        Console.WriteLine(creditCard == creditCard2 ? "Equal" : "Not equal");
        Console.WriteLine(creditCard > creditCard2
            ? "First card balance is bigger than second card balance"
            : "First card balance is smaller than second card balance");
        
    }

}