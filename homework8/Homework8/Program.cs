namespace Homework8;

class Program
{
    static void Main(string[] args)
    {
        BarberShop shop = new BarberShop(3);

        // Запускаємо перукаря в окремому потоці
        Thread barberThread = new Thread(new ThreadStart(shop.BarberWork));
        barberThread.Start();

        // Симулюємо прихід клієнтів
        Random rnd = new Random();
        int customerId = 1;

        while (customerId <= 15)
        {
            Thread.Sleep(rnd.Next(500, 2000)); // новий клієнт кожні 0.5-2 секунди
            int id = customerId;
            new Thread(() => shop.CustomerArrives(id)).Start();
            customerId++;
        }

        barberThread.Join();
    }

    class BarberShop
    {
        private readonly Queue<int> waitingRoom = new Queue<int>();
        private readonly int maxSeats;
        private bool barberSleeping = true;
        private readonly object lockObject = new object();
        
        public BarberShop(int seats)
        {
            maxSeats = seats;
        }

        public void CustomerArrives(int customerId)
        {
            lock (lockObject)
            {
                if (waitingRoom.Count < maxSeats)
                {
                    waitingRoom.Enqueue(customerId);
                    Console.WriteLine($"Клієнт {customerId} зайшов до перукарні і чекає.");
                    if (barberSleeping)
                    {
                        Console.WriteLine("Перукар прокидається.");
                        Monitor.Pulse(lockObject);
                    }
                }
                else
                {
                    Console.WriteLine($"Клієнт {customerId} пішов, немає місця");
                }
            }
        }
        
        public void BarberWork()
        {
            while (true)
            {
                int? customerId = null;

                lock (lockObject)
                {
                    while (waitingRoom.Count == 0)
                    {
                        Console.WriteLine("Перукар спить, чекає клієнтів...");
                        barberSleeping = true;
                        Monitor.Wait(lockObject); // засинає
                    }

                    barberSleeping = false;
                    customerId = waitingRoom.Dequeue();
                }

                Console.WriteLine($"Перукар стриже клієнта {customerId}.");
                Thread.Sleep(2000); // стрижка 3 секунди
                Console.WriteLine($"Клієнт {customerId} пострижений.");
            }
        }
        
    }
    
}