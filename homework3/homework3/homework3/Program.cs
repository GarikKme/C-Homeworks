using homework3.Task1;

namespace homework3;

class Program
{
    static void Main(string[] args)
    {
        int[] numbers = { 33, 55, 7, 8, 89, 123, 23, 34, 5, 99, 100 };
        MyArray array = new MyArray(numbers);
        //Task 1
        array.Show();
        Console.WriteLine();
        array.Show("This is information message");
        
        //Task 2
        Console.WriteLine($"\nМаксимум: {array.Max()}");
        Console.WriteLine($"Мінімум: {array.Min()}");
        Console.WriteLine($"Середнє: {array.Avg()}");
        
        int searchValue = 55;
        Console.WriteLine($"Чи є {searchValue} в масиві? {array.Search(searchValue)}");
        
        searchValue = 999;
        Console.WriteLine($"Чи є {searchValue} в масиві? {array.Search(searchValue)}");
        
        //Task 3
        Console.WriteLine("Array before sorting");
        array.Show();

        Console.WriteLine("Sort ASC");
        array.SortAsc();
        array.Show();

        Console.WriteLine("Sort DESC");
        array.SortDesc();
        array.Show();

        Console.WriteLine("Sort By Param (true):");
        array.SortByParam(true);
        array.Show();

        Console.WriteLine("Sort By Param (false):");
        array.SortByParam(false);
        array.Show();
    }
}