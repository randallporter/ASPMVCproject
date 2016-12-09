using AppointmentSetter.DataAccess;
using AppointmentSetter.Models;
using AppointmentSetter.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace AppointmentSetter.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentRepository _ar;
        private readonly IAppointmentTypeRepository _atr;
        private readonly IUserRepository _ur;
        private readonly AppointmentDBContext context;

        public AppointmentController()
        {
            context = new AppointmentDBContext();
            _ar = new AppointmentRepository(context);
            _atr = new AppointmentTypeRepository(context);
            _ur = new UserRepository(context);
        }

        [Authorize]
        [Route("Home/Index"), Route("Home")]
        public ActionResult Index()
        {
            //Get Only that users appointments or show all if the attender.
            var id = User.Identity.GetUserId();
            var beginDate = DateTime.Now.Date;

            List<Appointment> items = new List<Appointment>();

            items = _ar.AllIncluding(e => e.appointmentType, e => e.AppointmentSetter)
                .Where(e => (e.appointmentAttender.AppUserID == id | e.AppointmentSetter.AppUserID == id) 
                && e.StartDate > beginDate).ToList();

            return View(items);
        }


        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new AppointmentViewModel
            {
                AppointmentTypes = _atr.All.ToList()
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(AppointmentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.AppointmentTypes = _atr.All.ToList();
                return View("Create", viewModel);
            }
                

            AppointmentType apptType = _atr.Find(viewModel.AppointmentType);
            var aspUserID = User.Identity.GetUserId();

            var appointment = new Appointment
            {
                StartDate = viewModel.GetStartTime(),
                AppointmentSetter = _ur.All.Where(e=>e.AppUserID == aspUserID).First(),
                appointmentType = apptType,
                //TO DO: this should become a drop down
                appointmentAttender = _ur.All.Where(e=>e.IsCustomer == false).First(),
                Notes = viewModel.Notes,
                EndDate = viewModel.GetStartTime().Add(apptType.AppointmentLength)
            };

            _ar.InsertOrUpdate(appointment);
            _ar.Save();

            return RedirectToAction("Index", "Appointment");
        }

        // GET: Appointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = _ar.Find((int)id);
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
            AppointmentEditViewModel appointmentEdit = new AppointmentEditViewModel(_ar.Find((int)id));
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
                var appointment = _ar.AllIncluding(e => e.appointmentType, e => e.AppointmentSetter, e => e.appointmentAttender)
                    .Where(e => e.ID == appointmentEdit.ID).First();
                appointment.Notes = appointmentEdit.Notes;
                appointment.StartDate = appointmentEdit.StartDate;
                appointment.EndDate = appointmentEdit.EndDate;

                _ar.InsertOrUpdate(appointment);
                _ar.Save();
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
            Appointment appointment = _ar.Find((int)id);
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
            _ar.Delete(id);
            _ar.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _ar.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}