using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HMS.Models
{
    public class Doctor
    {
        [Key]
        public int Doctor_ID { get; set; }
        public string? Doctor_Fname { get; set; }
        public string? Doctor_Lname { get; set; }
        public string? Doctor_Qualification { get; set; }
        public string? Doctor_Specialization { get; set; }
        public DateOnly Doctor_DateJoining { get; set; }
        public string? Doctor_Email { get; set; }
        [JsonIgnore]
        public Department? Doctor_Department_Id { get; set; }
        public List<Appointment>? Doctor_Appointments_Id { get; set; }
    }
}
