namespace Homework4.Task2;

public class City
{
    public string Name { get; set; }
    public int Population { get; set; }

    public City(string name, int population)
    {
        Name = name;
        Population = population;
    }
    
    // Operator overload +
    public static City operator + (City city, int amount)
    {
        city.Population += amount;
        return city;
    }

    // Operator overload -
    public static City operator -(City city, int amount)
    {
        city.Population -= amount;
        return city;
    }
    
    // Operator overload ==
    public static bool operator == (City city1, City city2)
    {
        return city1.Population == city2.Population;
    }
    
    // Operator overload !=
    public static bool operator != (City city1, City city2)
    {
        return city1.Population != city2.Population;
    }
    
    // Operator overload >
    public static bool operator > (City city1, City city2)
    {
        return city1.Population > city2.Population;
    }
    
    // Operator overload <
    public static bool operator < (City city1, City city2)
    {
        return city1.Population < city2.Population;
    }
    
    //overload for methods
    public override bool Equals(object? obj)
    {
        if (obj is City otherCity) return Population == otherCity.Population;
        return false;
    }

    public override int GetHashCode() => Population.GetHashCode();
    public override string ToString() => $"{Name} — Population: {Population}";
}