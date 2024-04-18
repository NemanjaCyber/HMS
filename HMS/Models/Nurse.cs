using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HMS.Models
{
    public class Nurse
    {
        [Key]
        public int Nurse_ID { get; set; }
        public string? Nurse_Fname { get; set; }
        public string? Nurse_Lname { get; set; }
        public DateOnly Nurse_DateJoining { get; set; }
        public string? Nurse_Email { get; set; }
        [JsonIgnore]
        public Department? Nurse_Department_Id { get; set; }
    }
}
