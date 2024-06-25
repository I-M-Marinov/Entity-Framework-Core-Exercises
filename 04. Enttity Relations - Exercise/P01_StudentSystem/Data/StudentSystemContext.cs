using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data
{
    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        {

        }

        public StudentSystemContext(DbContextOptions<StudentSystemContext> options)
            : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* Student */

            modelBuilder.Entity<Student>()
                .HasKey(s => s.StudentId);

            modelBuilder.Entity<Student>()
                .Property(s => s.Name)
                .HasMaxLength(100)
                .IsUnicode(true)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .Property(s => s.PhoneNumber)
                .HasColumnType("CHAR")
                .HasMaxLength(10)
                .IsFixedLength(true); // default value for the IsFixedLength is TRUE, so we can be explicit or not 

            modelBuilder.Entity<Student>()
                .Property(s => s.RegisteredOn)
                .ValueGeneratedOnAdd()
                .IsRequired();

            modelBuilder.Entity<Student>()
                .Property(s => s.Birthday)
                .IsRequired(false);

            /* Course */

            modelBuilder.Entity<Course>()
                .HasKey(c => c.CourseId);

            modelBuilder.Entity<Course>()
                .Property(c => c.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(80);

            modelBuilder.Entity<Course>()
                .Property(c => c.Description)
                .IsUnicode(true)
                .IsRequired(false);

            modelBuilder.Entity<Course>()
                .Property(c => c.StartDate)
                .IsRequired(true);

            modelBuilder.Entity<Course>()
                .Property(c => c.EndDate)
                .IsRequired(true);

            modelBuilder.Entity<Course>()
                .Property(c => c.Price)
                .IsRequired(true);

            /* Course */


            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (optionsBuilder.IsConfigured == false)
            {
                string connectionString = "Server=MARINOV-GAME-PC\\SQLEXPRESS; Database = BlogDb; Integrated Security = true; Encrypt = False; TrustServerCertificate = true;";

                optionsBuilder.UseSqlServer(connectionString);

            }
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}
