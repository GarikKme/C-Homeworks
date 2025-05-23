namespace Homework4.Task1;

public class Employee
{
    public string Name { get; set; }
    public string Position { get; set; }
    public double Salary { get; set; }

    public Employee(string name, string position, double salary)
    {
        Name = name;
        Position = position;
        Salary = salary;
    }

    // Operator overload +
    public static Employee operator +(Employee employee, double amount)
    {
        employee.Salary += amount;
        return employee;
    }

    // Operator overload -
    public static Employee operator -(Employee employee, double amount)
    {
        employee.Salary -= amount;
        return employee;
    }

    // Operator overload ==
    public static bool operator ==(Employee employee1, Employee employee2)
    {
        return employee1.Salary == employee2.Salary;
    }

    // Operator overload !=
    public static bool operator !=(Employee employee1, Employee employee2)
    {
        return employee1.Salary != employee2.Salary;
    }

    // Operator overload >
    public static bool operator >(Employee e1, Employee e2)
    {
        return e1.Salary > e2.Salary;
    }

    // Operator overload <
    public static bool operator <(Employee e1, Employee e2)
    {
        return e1.Salary < e2.Salary;
    }

    //overload for methods

    public override bool Equals(object? obj)
    {
        if (obj is Employee other)
        {
            return Salary == other.Salary;
        }

        return false;
    }

    public override int GetHashCode() => Salary.GetHashCode();
    public override string ToString() => $"Name: {Name}, Position: {Position}, Salary: {Salary}";
}