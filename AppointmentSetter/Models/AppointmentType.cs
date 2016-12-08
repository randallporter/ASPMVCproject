using System;
using System.ComponentModel.DataAnnotations;

namespace AppointmentSetter.Models
{
    public class AppointmentType
    {
        [Key]
        public byte ID { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [ScaffoldColumn(false)]
        public TimeSpan AppointmentLength { get; set; }

    }
}