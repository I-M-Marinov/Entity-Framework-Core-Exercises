using Medicines.Data.Models;

namespace Medicines.Data
{
    using Microsoft.EntityFrameworkCore;
    using System.Net;

    public class MedicinesContext : DbContext
    {
        public MedicinesContext()
        {
        }

        public MedicinesContext(DbContextOptions options)
            : base(options)
        {
        }


        public DbSet<Medicine> Medicines { get; set; } = null!;
        public DbSet<Patient> Patients { get; set; } = null!;
        public DbSet<Pharmacy> Pharmacies { get; set; } = null!;
        public DbSet<PatientMedicine> PatientsMedicines { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies() // lazy loading 
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PatientMedicine>()
                .HasKey(pm => new { pm.PatientId, pm.MedicineId }); // Composite key for the mapping table 
        }
    }
}
