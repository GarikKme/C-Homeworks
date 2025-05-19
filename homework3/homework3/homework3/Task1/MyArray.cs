using homework3.Task2;
using homework3.Task3;

namespace homework3.Task1;

public class MyArray : IOutput, IMath, ISort
{
    private int[] numbers;

    public MyArray(int[] initialArray)
    {
        numbers = initialArray;
    }

    //Task 1
    public void Show()
    {
        Console.WriteLine(string.Join(", ", numbers));
    }

    public void Show(string info)
    {
        Console.WriteLine(info);
        Show();
    }

    //Task 2
    public int Max()
    {
        return numbers.Max();
    }

    public int Min()
    {
        return numbers.Min();
    }

    public float Avg()
    {
        return (float)numbers.Average();
    }

    public bool Search(int valueToSearch)
    {
        return numbers.Contains(valueToSearch);
    }

    //Task 3
    public void SortAsc()
    {
        Array.Sort(numbers);
    }

    public void SortDesc()
    {
        Array.Sort(numbers);
        Array.Reverse(numbers);
    }

    public void SortByParam(bool isAsc)
    {
        if (isAsc)
            SortAsc();
        else
            SortDesc();
    }
}