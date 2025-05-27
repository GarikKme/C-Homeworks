using MyDoctorAppointment.Domain.Entities;
using MyDoctorAppointment.Service.Interfaces;
using MyDoctorAppointment.Service.Services;

namespace MyDoctorAppointment
{
    public class DoctorAppointment
    {
        private readonly IDoctorService _doctorService;
        public enum MenuOption
        {
            ShowDoctors = 1,
            AddDoctor = 2,
            Exit = 3
        }

        public DoctorAppointment()
        {
            _doctorService = new DoctorService();
        }

        public void Menu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Menu ---");
                Console.WriteLine("1. Show Doctors");
                Console.WriteLine("2. Add a Doctor");
                Console.WriteLine("3. Exit");
                Console.Write("Choose one (1-3): ");

                string? input = Console.ReadLine();

                if (!int.TryParse(input, out int option) || !Enum.IsDefined(typeof(MenuOption), option))
                {
                    Console.WriteLine("Wrong input, please try again.");
                    continue;
                }

                switch ((MenuOption)option)
                {
                    case MenuOption.ShowDoctors:
                        ShowDoctors();
                        break;

                    case MenuOption.AddDoctor:
                        AddDoctor();
                        break;

                    case MenuOption.Exit:
                        Console.WriteLine("Выход из программы...");
                        return;
                }
            }
        }

        private void ShowDoctors()
        {
            var docs = _doctorService.GetAll();

            if (!docs.Any())
            {
                Console.WriteLine("No doctors found.");
                return;
            }

            Console.WriteLine("Current doctors list:");
            foreach (var doc in docs)
            {
                Console.WriteLine($"- {doc.Name} {doc.Surname}, Experience: {doc.Experience} years, Type: {doc.DoctorType}");
            }
        }

        private void AddDoctor()
        {
            Console.WriteLine("Adding a new doctor...");

            Console.Write("Enter doctor's name: ");
            string? name = Console.ReadLine() ?? "";

            Console.Write("Enter doctor's surname: ");
            string? surname = Console.ReadLine() ?? "";

            byte experience;
            while (true)
            {
                Console.Write("Enter doctor's experience (years): ");
                string? expInput = Console.ReadLine();
                if (byte.TryParse(expInput, out experience) && experience >= 0)
                {
                    break;
                }
                Console.WriteLine("Invalid experience value. Please enter a non-negative integer.");
            }

            Console.WriteLine("Choose doctor type:");
            foreach (var value in Enum.GetValues(typeof(Domain.Enums.DoctorTypes)))
            {
                Console.WriteLine($"{(int)value}. {value}");
            }

            Domain.Enums.DoctorTypes doctorType;
            while (true)
            {
                Console.Write("Enter the number corresponding to the doctor type: ");
                string? typeInput = Console.ReadLine();
                if (int.TryParse(typeInput, out int typeInt) && Enum.IsDefined(typeof(Domain.Enums.DoctorTypes), typeInt))
                {
                    doctorType = (Domain.Enums.DoctorTypes)typeInt;
                    break;
                }
                Console.WriteLine("Invalid doctor type. Please try again.");
            }

            var newDoctor = new Doctor
            {
                Name = name,
                Surname = surname,
                Experience = experience,
                DoctorType = doctorType
            };

            _doctorService.Create(newDoctor);
            Console.WriteLine($"Doctor {name} {surname} added successfully!");
        }
    }

    public static class Program
    {
        public static void Main()
        {
            var doctorAppointment = new DoctorAppointment();
            doctorAppointment.Menu();
        }
    }
}
