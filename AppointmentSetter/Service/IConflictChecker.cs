using AppointmentSetter.DataAccess;
using AppointmentSetter.Models;
using System;

namespace AppointmentSetter.Service
{
    public interface IConflictChecker
    {
        //void SetDataRepo(IAppointmentRepository ar);
        Appointment GetAttenderConflict(User attender, DateTime start, DateTime end);
        Appointment GetSetterConflict(User setter, DateTime start, DateTime end);
    }
}
