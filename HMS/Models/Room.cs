using System.ComponentModel.DataAnnotations;

namespace HMS.Models
{
    public class Room
    {
        [Key]
        public int Room_ID { get; set; }
        public string Room_Type { get; set; }
        public decimal Room_Cost { get; set; }
        public int Room_Available_Beds { get; set; }
        public List<Patient>? Room_Patients { get; set; }
    }
}
