using System.ComponentModel.DataAnnotations;

namespace AppointmentSetter.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        public string AppUserID { get; set; }

        public string userName { get; set; }

        public bool IsCustomer { get; set;}

    }
}