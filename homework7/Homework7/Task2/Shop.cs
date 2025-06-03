namespace Homework7.Task2;

public class Shop : IDisposable
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Type { get; set; }
    
    private bool disposed = false;

    public Shop(string name, string address, string type)
    {
        Name = name;
        Address = address;
        Type = type;
    }

    public void GetInfo()
    {
        Console.WriteLine($"This is a {Name}. Its a {Type} shop");
        Console.WriteLine($"It's address is {Address}");
    }
    
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
                Console.WriteLine($"Dispose called for a shop \"{Name}\" (managed resources).");
            }

            // Тут звільняються некеровані ресурси, якщо є
            Console.WriteLine($"Shop \"{Name}\" destroyed (unmanaged resources).");

            disposed = true;
        }
    }

    // Task 3
    ~Shop()
    {
        Dispose(false);
    }
    
}