using AppointmentSetter.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Appointment
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Event Notes")]
        public string Notes { get; set; }

        [Display(Name = "Start Date"), Required]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date"), Required]
        public DateTime EndDate { get; set; }

        [Required]
        public AppointmentType appointmentType { get; set; }

        [Required]
        public ApplicationUser AppointmentSetter { get; set; }

        [Required]
        public AppointmentAttender appointmentAttender { get; set; }
    }
}