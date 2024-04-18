using System.ComponentModel.DataAnnotations;

namespace HMS.Models
{
    public class Department
    {
        [Key]
        public int Department_ID { get; set; }
        public string? Department_Name { get; set; }
        public int Department_EmplCount { get; set; }
        public List<Doctor>? Department_Doctors_Id { get; set; }
        public List<Nurse>? Department_Nurses_Id { get; set; }
    }
}
