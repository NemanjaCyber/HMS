using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HMS.Models
{
    public class Patient
    {
        [Key]
        public int Patient_ID { get; set; }
        public string Patient_Fname { get; set; }
        public string Patient_Lname { get; set; }
        public string JMBG { get; set; }
        public string Blood_type { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Condition { get; set; }
        public DateTime Admision_Date { get; set; }
        public DateTime Discharge_Date { get; set; }
        public string Phone { get; set; }
        [JsonIgnore]
        public Room? Assigned_Room_Id { get; set; }
    }
}
