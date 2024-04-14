using Microsoft.EntityFrameworkCore;

namespace HMS.Models
{
    public class HMSContext:DbContext
    {
        public DbSet<Patient>? Patients { get; set; }
        public DbSet<MedicalHistory>? MedicalHistories { get; set; }

        public HMSContext(DbContextOptions options) : base(options) { }
    }
}
