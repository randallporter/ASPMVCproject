using AppointmentSetter.Models;
using AppointmentSetter.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;
using WebApp.Models;

namespace AppointmentSetter.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new AppointmentViewModel
            {
                AppointmentTypes = _context.AppointmentTypes.ToList()
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(AppointmentViewModel viewModel)
        {
            var appointmentSetter = _context.Users.Find(User.Identity.GetUserId());
            DateTime apptDate = DateTime.Parse(string.Format("{0} {1}", viewModel.Date, viewModel.Time));
            AppointmentType apptType = _context.AppointmentTypes.Find(viewModel.AppointmentType);

            var appointment = new Appointment
            {
                StartDate = apptDate,
                AppointmentSetter = appointmentSetter,
                appointmentType = apptType,
                appointmentAttender = _context.AppointmentAttenders.First(),
                Notes = viewModel.Notes,
                EndDate = apptDate.Add(apptType.AppointmentLength)
            };

            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}