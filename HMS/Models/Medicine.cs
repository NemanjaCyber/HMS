using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace HMS.Models
{
    public class Medicine
    {
        [Key]
        public int Medicine_ID { get; set; }
        public string? M_Name { get; set; }
        public int M_Quantity { get; set; }
        public decimal M_Cost { get; set; }
        public List<Prescription>? Assigned_To_Prescriptions { get; set; }
    }
}
