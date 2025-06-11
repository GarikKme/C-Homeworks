using System.Text.Json;
using MyDoctorAppointment.Data.Interfaces;

namespace DoctorAppointmentDemo.Data.Serialization;

public class JsonDataSerializer : ISerializer
{
    public string Serialize<T>(IEnumerable<T> data)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        return JsonSerializer.Serialize(data, options);
    }
    
    public IEnumerable<T> Deserialize<T>(string content)
    {
        return JsonSerializer.Deserialize<List<T>>(content)!;
    }
}