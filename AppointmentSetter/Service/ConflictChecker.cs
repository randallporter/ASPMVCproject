using AppointmentSetter.DataAccess;
using AppointmentSetter.Models;
using System;

namespace AppointmentSetter.Service
{
    public class ConflictChecker : IConflictChecker
    {
        private IAppointmentRepository _repo;

        public ConflictChecker(IAppointmentRepository repo)
        {
            _repo = repo;
        }

        Appointment IConflictChecker.CheckAttenderConflict(User attender, DateTime start, TimeSpan length)
        {
            throw new NotImplementedException();
        }

        Appointment IConflictChecker.CheckSetterConflict(User setter, DateTime start, TimeSpan length)
        {
            throw new NotImplementedException();
        }
    }
}