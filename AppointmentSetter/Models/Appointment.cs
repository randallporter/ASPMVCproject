using System;
using System.ComponentModel.DataAnnotations;

namespace AppointmentSetter.Models
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
        [Display(Name = "Appointment Type")]
        public AppointmentType appointmentType { get; set; }

        [Required]
        [Display(Name = "Appointment Requester")]
        public User AppointmentSetter { get; set; }

        [Required]
        public User appointmentAttender { get; set; }
    }
}