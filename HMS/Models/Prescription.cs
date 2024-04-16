using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HMS.Models
{
    public class Prescription
    {
        [Key]
        public int Prescription_ID { get; set; }
        public DateTime Date { get; set; }
        public List<Medicine>? Assigned_Medicines { get; set; }
        public Patient? Assigned_Patient { get; set; }
    }
}
