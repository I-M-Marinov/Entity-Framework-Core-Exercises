﻿using Trucks.Data.Models;

namespace Trucks.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;

    public class TrucksContext : DbContext
    {
        public TrucksContext()
        { 
        }

        public TrucksContext(DbContextOptions options)
            : base(options) 
        { 
        }

        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Despatcher> Despatchers { get; set; } = null!;
        public DbSet<Truck> Trucks { get; set; } = null!;
        public DbSet<ClientTruck> ClientsTrucks { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies() // Initiate lazy loading 
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientTruck>()
                .HasKey(ct => new { ct.ClientId, ct.TruckId }); // composite key for the mapping table 
        }
    }
}
