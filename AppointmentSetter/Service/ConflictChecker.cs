using AppointmentSetter.DataAccess;
using AppointmentSetter.Models;
using System;
using System.Linq;

namespace AppointmentSetter.Service
{
    public class ConflictChecker : IConflictChecker
    {
        private IAppointmentRepository _repo;
        TimeSpan EndBuffer;
        TimeSpan StartBuffer;

        public ConflictChecker(IAppointmentRepository repo)
        {
            _repo = repo;
            repo.setContext(new AppointmentDBContext());
            EndBuffer = new TimeSpan(1, 0, 0);
            StartBuffer = new TimeSpan(1, 0, 0);

        }

        public Appointment GetAttenderConflict(User attender, DateTime start, DateTime end)
        {
            Appointment appointment = new Appointment();
            start = start.Subtract(StartBuffer);
            end = end.Add(EndBuffer);

            appointment = _repo.All.Where(e => e.appointmentAttender.ID == attender.ID 
                && ((e.StartDate < start && start < e.EndDate) 
                | (e.StartDate < end && end < e.EndDate)
                | (start < e.StartDate && e.EndDate < end))).FirstOrDefault();
            return appointment;
        }

        public Appointment GetSetterConflict(User setter, DateTime start, DateTime end)
        {
            Appointment appointment = new Appointment();
            start = start.Subtract(StartBuffer);
            end = end.Add(EndBuffer);

            appointment = _repo.All.Where(e => e.AppointmentSetter.ID == setter.ID
                && ((e.StartDate < start && start < e.EndDate)
                | (e.StartDate < end && end < e.EndDate)
                | (start < e.StartDate && e.EndDate < end))).FirstOrDefault();
            return appointment;
        }
    }
}