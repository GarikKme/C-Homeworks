namespace Homework7.Task1;

public class Play : IDisposable
{
    public string Name { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public int Year { get; set; }
    
    private bool disposed = false;

    public Play(string name, string author, string genre, int year)
    {
        Name = name;
        Author = author;
        Genre = genre;
        Year = year;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Genre: {Genre}");
        Console.WriteLine($"Year: {Year}");
    }
        
    // Destructor
    ~Play()
    {
        Console.WriteLine($"Play's object \"{Name}\" disposed...");
    }
    
    //Task 3
    
    // Dispose
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this); // повідомляємо GC, що фіналізатор не потрібен
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                // Тут звільняємо керовані ресурси, якщо вони є (файли, потоки тощо)
                Console.WriteLine($"Dispose called for a play \"{Name}\" (managed resources).");
            }

            // Тут звільняються некеровані ресурси, якщо є
            Console.WriteLine($"Play \"{Name}\" destroyed (unmanaged resources).");

            disposed = true;
        }
    }
}