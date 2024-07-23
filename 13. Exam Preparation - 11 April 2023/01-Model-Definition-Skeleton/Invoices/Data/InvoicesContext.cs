﻿using Invoices.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Invoices.Data
{
    public class InvoicesContext : DbContext
    {
        public InvoicesContext() 
        { 
        }

        public InvoicesContext(DbContextOptions options)
            : base(options)
        { 
        }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Address> Addresses { get; set; } = null!;
        public DbSet<Invoice> Invoices { get; set; } = null!;
        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<ProductClient> ProductsClients { get; set; } = null!;



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // setup composite primary key using the FLUENT API 
            modelBuilder.Entity<ProductClient>()
                .HasKey(pc => new { pc.ProductId, pc.ClientId });

        }
    }
}
