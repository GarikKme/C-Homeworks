using Homework7.Task1;
using Homework7.Task2;

namespace Homework7;

class Program
{
    static void Main(string[] args)
    {
        //Task 1 
        Play play1 = new Play("Game of Thrones", "Billy Jean", "Horror", 2003);
        play1.ShowInfo();
        
        play1 = null;
        
        //GC
        GC.Collect();
        GC.WaitForPendingFinalizers();
        
        Console.WriteLine("Press any key to continue...");
        
        //Task 2
        
        // Вариант 1: ручний виклик Dispose
        var shop1 = new Shop("Сільпо", "вул. Центральна, 10", "продовольчий");
        shop1.GetInfo();
        shop1.Dispose(); // ручне очищення

        // Вариант 2: автоматичне очищення через using
        using (var shop2 = new Shop("Епіцентр", "вул. Будівельників, 5", "господарський"))
        {
            shop2.GetInfo();
        } // тут Dispose викликається автоматично

        Console.WriteLine("Press any key to continue...");
        
        //Task 3
        
        var play2 = new Play("Gamlet", "Dostoevskiy", "Comedy", 1844);
        play2.ShowInfo();
        play2.Dispose();

        using (var play3 = new Play("Gamlet", "Dostoevskiy", "Comedy", 1844))
        {
            play3.ShowInfo();
        }
        
        //Task3 for task 2

        var shop3 = new Shop("ATB", "Вокзальна 32б", "Продовльчий");
        
        shop3 = null;
        
        //GC
        GC.Collect();
        GC.WaitForPendingFinalizers();
        
        Console.WriteLine("Press any key to continue...");
    }

   
}