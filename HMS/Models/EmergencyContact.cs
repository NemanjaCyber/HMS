using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HMS.Models
{
    public class EmergencyContact
    {
        [Key]
        public int EmergencyContact_ID { get; set; }
        public string? EmergencyContact_Name { get; set; }
        public string? EmergencyContact_Phone { get; set; }
        public string? EmergencyContact_Realtion { get; set; }
        [JsonIgnore]
        public Patient? RelatedPatient_Id { get; set; }
    }
}
