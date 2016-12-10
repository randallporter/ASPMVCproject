using AppointmentSetter.Models;
using System;

namespace AppointmentSetter.Service
{
    interface IConflictChecker
    {
        Appointment GetAttenderConflict(User attender, DateTime start, DateTime end);
        Appointment GetSetterConflict(User setter, DateTime start, DateTime end);
    }
}
