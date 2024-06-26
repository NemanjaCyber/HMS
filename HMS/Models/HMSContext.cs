﻿using Microsoft.EntityFrameworkCore;

namespace HMS.Models
{
    public class HMSContext:DbContext
    {
        public DbSet<Patient>? Patients { get; set; }
        public DbSet<MedicalHistory>? MedicalHistories { get; set; }
        public DbSet<Room>? Rooms { get; set; }
        public DbSet<Medicine>? Medicines { get; set; }
        public DbSet<Prescription>? Prescriptions { get; set; }
        public DbSet<EmergencyContact>? EmergencyContacts { get; set; }
        public DbSet<Department>? Departments { get; set; }
        public DbSet<Nurse>? Nurses { get; set; }
        public DbSet<Doctor>? Doctors { get; set; }
        public DbSet<Appointment>? Appointments { get; set; }
        public HMSContext(DbContextOptions options) : base(options) { }
    }
}
