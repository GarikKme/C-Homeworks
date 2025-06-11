using MyDoctorAppointment.Domain.Entities;

namespace MyDoctorAppointment.Data.Interfaces;

public interface ISerializer
{
    string Serialize<T>(IEnumerable<T> data);
    IEnumerable<T> Deserialize<T>(string content);
}