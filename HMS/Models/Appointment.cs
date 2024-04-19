using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HMS.Models
{
    public class Appointment
    {
        [Key]
        public int Appointment_ID { get; set; }
        public DateOnly Appointment_Date { get; set; }
        public TimeOnly Appointment_Time { get; set; }
        [JsonIgnore]
        public Doctor? Appointment_With_Doctor { get; set; }
        [JsonIgnore]
        public Patient? Appointment_With_Patient { get; set; }
    }
}
