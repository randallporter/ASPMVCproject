using System.ComponentModel.DataAnnotations;

namespace AppointmentSetter.Models
{
    public class AppointmentAttender
    {   
        [Key]
        public int ID { get; set; }

        [Required]
        public ApplicationUser appointmentAttender { get; set; }

        [Required]
        public string Title { get; set; }
    }
}