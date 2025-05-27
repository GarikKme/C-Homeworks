using MyDoctorAppointment.Data.Configuration;
using MyDoctorAppointment.Data.Interfaces;
using MyDoctorAppointment.Domain.Entities;

namespace MyDoctorAppointment.Data.Repositories;

public class PatientRepository : GenericRepository<Patient>, IPatientRepository

{
    public override string Path { get; set; }
    
    public override int LastId { get; set; }

    public PatientRepository()
    {
        dynamic result = ReadFromAppSettings();
        LastId = result.LastId;
        Path = result.Path;
    }

    public override void ShowInfo(Patient patient)
    {
        Console.WriteLine($"Patient Id: {patient.Id}");
    }
    protected override void SaveLastId()
    {
        dynamic result = ReadFromAppSettings();
        result.Database.Appointments.LastId = LastId;

        File.WriteAllText(Constants.AppSettingsPath, result.ToString());
    }
}