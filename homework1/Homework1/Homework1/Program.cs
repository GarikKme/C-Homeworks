using System;
namespace Homework1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Вітаю! Це калькулятор. Введіть будь-ласка два числа і він проведе операцію з ними.");
            double firstNumber = Convert.ToDouble(Console.ReadLine());
            double secondNumber = Convert.ToDouble(Console.ReadLine());

            IOperation add = new Addition(firstNumber, secondNumber);
            IOperation subtract = new Subtraction(firstNumber, secondNumber);
            IOperation multiply = new Multiplication(firstNumber, secondNumber);
            IOperation divide = new Division(firstNumber, secondNumber);

            Calculator calculator = new Calculator();
            Console.WriteLine($"Додавання: {calculator.Execute(add)}");
            Console.WriteLine($"Віднімання: {calculator.Execute(subtract)}");
            Console.WriteLine($"Множення: {calculator.Execute(multiply)}");
            Console.WriteLine($"Ділення: {calculator.Execute(divide)}");
        }
    }

    // Interface
    public interface IOperation
    {
        double Calculate();
    }

    // Classes
    public class Addition : IOperation
    {
        private double _a, _b;

        public Addition(double a, double b)
        {
            _a = a;
            _b = b;
        }

        public double Calculate() => _a + _b;
    }

    public class Subtraction : IOperation
    {
        private double _a, _b;

        public Subtraction(double a, double b)
        {
            _a = a;
            _b = b;
        }

        public double Calculate() => _a - _b;
    }

    public class Multiplication : IOperation
    {
        private double _a, _b;

        public Multiplication(double a, double b)
        {
            _a = a;
            _b = b;
        }

        public double Calculate() => _a * _b;
    }

    public class Division : IOperation
    {
        private double _a, _b;

        public Division(double a, double b)
        {
            _a = a;
            _b = b;
        }

        public double Calculate()
        {
            if (_b == 0)
                throw new DivideByZeroException("Ділення на ноль недоступне!");
            return _a / _b;
        }
    }

    // Calculator
    public class Calculator
    {
        public double Execute(IOperation operation)
        {
            return operation.Calculate();
        }
    }
}
