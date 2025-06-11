using System.Xml.Serialization;
using MyDoctorAppointment.Data.Interfaces;

namespace DoctorAppointmentDemo.Data.Serialization;

public class XmlDataSerializer : ISerializer
{
    public string Serialize<T>(IEnumerable<T> data)
    {
        var serializer = new XmlSerializer(typeof(List<T>));
        using var sw = new StringWriter();
        serializer.Serialize(sw, data.ToList());
        return sw.ToString();
    }
    public IEnumerable<T> Deserialize<T>(string content)
    {
        var serializer = new XmlSerializer(typeof(List<T>));
        using var sr = new StringReader(content);
        return (List<T>)serializer.Deserialize(sr)!;
    }
}