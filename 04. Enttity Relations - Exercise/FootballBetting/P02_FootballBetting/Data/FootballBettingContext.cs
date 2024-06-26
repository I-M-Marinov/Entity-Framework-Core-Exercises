using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using P02_FootballBetting.Data.Models;

namespace P02_FootballBetting.Data
{
    internal class FootballBettingContext: DbContext
    {

        public FootballBettingContext()
        {

        }

        public FootballBettingContext(DbContextOptions<FootballBettingContext> options)
            : base(options)
        {


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (optionsBuilder.IsConfigured == false)
            {
                string connectionString = "Server=MARINOV-GAME-PC\\SQLEXPRESS; Database = BlogDb; Integrated Security = true; Encrypt = False; TrustServerCertificate = true;";

                optionsBuilder.UseSqlServer(connectionString);

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* TEAM */

            modelBuilder.Entity<Team>()
            .HasKey(t => t.TeamId);

            modelBuilder.Entity<Team>()
                .Property(t => t.Name)
                .HasColumnType("VARCHAR")
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Team>()
                .Property(t => t.LogoUrl)
                .HasColumnType("NVARCHAR")
                .IsRequired();

            modelBuilder.Entity<Team>()
                .Property(t => t.Initials)
                .HasColumnType("CHAR")
                .HasMaxLength(3)
                .IsRequired();

            modelBuilder.Entity<Team>()
                .Property(t => t.Budget)
                .HasColumnType("MONEY")
                .IsRequired();

            modelBuilder.Entity<Team>()
                .Property(t => t.PrimaryKitColorId)
                .HasColumnType("INT")
                .IsRequired();

            modelBuilder.Entity<Team>()
                .Property(t => t.SecondaryKitColorId)
                .HasColumnType("INT")
                .IsRequired();

            modelBuilder.Entity<Team>()
                .Property(t => t.TownId)
                .HasColumnType("INT")
                .IsRequired();

            /* COLOR */

            modelBuilder.Entity<Color>()
                .HasKey(t => t.ColorId);

            modelBuilder.Entity<Color>()
                .Property(t => t.Name)
                .HasColumnType("VARCHAR")
                .HasMaxLength(30)
                .IsRequired();


            base.OnModelCreating(modelBuilder);
        }


    }
}
