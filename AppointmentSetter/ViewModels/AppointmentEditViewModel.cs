using AppointmentSetter.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace AppointmentSetter.ViewModels
{
    public class AppointmentEditViewModel
    {
        public AppointmentEditViewModel()
        {

        }

        public AppointmentEditViewModel(Appointment appointment)
        {
            ID = appointment.ID;
            Notes = appointment.Notes;
            StartDate = appointment.StartDate;
            EndDate = appointment.EndDate;
        }

        public int ID { get; set; }

        public string Notes { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }



    }
}