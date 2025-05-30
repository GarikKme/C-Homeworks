﻿using MyDoctorAppointment.Domain.Entities;


namespace MyDoctorAppointment.Data.Interfaces
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        // you can add more specific Appointment's methods
    }
}
