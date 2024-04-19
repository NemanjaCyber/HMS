using System.ComponentModel.DataAnnotations;

namespace HMS.Models
{
    public class MedicalHistory
    {
        [Key]
        public int Record_ID { get; set; }
        public string? Alergies { get; set; }
        public string? Pre_Conditions { get; set; }
        public Patient? Patient_Id { get; set; }
    }
}
