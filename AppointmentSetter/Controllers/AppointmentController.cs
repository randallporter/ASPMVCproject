using AppointmentSetter.Models;
using AppointmentSetter.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

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
        [Route("Home/Index"), Route("Home")]
        public ActionResult Index()
        {
            //Get Only that users appointments or show all if the attender.
            var id = User.Identity.GetUserId();
            var beginDate = DateTime.Now.Date;

            List<Appointment> items = new List<Appointment>();

            items = _context.Appointments
                .Include(e => e.appointmentType).Include(e=>e.AppointmentSetter).Include(e=>e.appointmentAttender)
                .Where(e => (e.AppointmentSetter.Id == id | e.appointmentAttender.appointmentAttender.Id == id) 
                && e.StartDate > beginDate).ToList();

            return View(items);
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
            if (!ModelState.IsValid)
            {
                viewModel.AppointmentTypes = _context.AppointmentTypes.ToList();
                return View("Create", viewModel);
            }
                

            AppointmentType apptType = _context.AppointmentTypes.Find(viewModel.AppointmentType);

            var appointment = new Appointment
            {
                StartDate = viewModel.GetStartTime(),
                AppointmentSetter = _context.Users.Find(User.Identity.GetUserId()),
                appointmentType = apptType,
                appointmentAttender = _context.AppointmentAttenders.First(),
                Notes = viewModel.Notes,
                EndDate = viewModel.GetStartTime().Add(apptType.AppointmentLength)
            };

            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            return RedirectToAction("Index", "Appointment");
        }

        // GET: Appointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = _context.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppointmentEditViewModel appointmentEdit = new AppointmentEditViewModel(_context.Appointments.Find(id));
            if (appointmentEdit == null)
            {
                return HttpNotFound();
            }


            return View(appointmentEdit);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AppointmentEditViewModel appointmentEdit)
        {

            if (ModelState.IsValid)
            {
                var appointment = _context.Appointments.Where(e => e.ID == appointmentEdit.ID)
                    .Include(e => e.AppointmentSetter).Include(e => e.appointmentAttender).Include(e => e.appointmentType).First();
                appointment.Notes = appointmentEdit.Notes;
                appointment.StartDate = appointmentEdit.StartDate;
                appointment.EndDate = appointmentEdit.EndDate;

                _context.Entry(appointment).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appointmentEdit);
        }

        // GET: Appointments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = _context.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = _context.Appointments.Find(id);
            _context.Appointments.Remove(appointment);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}