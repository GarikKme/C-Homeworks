using MyDoctorAppointment.Domain.Entities;

namespace MyDoctorAppointment.Data.Interfaces;

public interface IPatientRepository : IGenericRepository<Patient>
{
    // you can add more specific Patient's methods
}