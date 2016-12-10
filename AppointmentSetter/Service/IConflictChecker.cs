using AppointmentSetter.Models;
using System;

namespace AppointmentSetter.Service
{
    interface IConflictChecker
    {
        Appointment CheckAttenderConflict(User attender, DateTime start, TimeSpan length);
        Appointment CheckSetterConflict(User setter, DateTime start, TimeSpan length);
    }
}
