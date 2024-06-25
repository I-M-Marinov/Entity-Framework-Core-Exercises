using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Reflection.Metadata;
using System.Security.AccessControl;
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


        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Resource> Resources { get; set; } = null!;
        public DbSet<Homework> Homeworks { get; set; } = null!;
        public DbSet<StudentCourse> StudentsCourses { get; set; } = null!;

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
            /* Student */

            modelBuilder.Entity<Student>()
                .HasKey(s => s.StudentId);

            modelBuilder.Entity<Student>()
                .Property(s => s.Name)
                .HasMaxLength(100)
                .IsUnicode()
                .IsRequired();

            modelBuilder.Entity<Student>()
                .Property(s => s.PhoneNumber)
                .HasColumnType("CHAR(10)")
                .IsFixedLength()
                .IsUnicode(false)
                .IsRequired(false); // default value for the IsFixedLength is TRUE, so we can be explicit or not 

            modelBuilder.Entity<Student>()
                .Property(s => s.RegisteredOn)
                .ValueGeneratedOnAdd()
                .IsRequired(); // default value for the IsRequired is TRUE, so we can be explicit or not 

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


            /* Resource */


            modelBuilder.Entity<Resource>()
                .HasKey(r => r.ResourceId);

            modelBuilder.Entity<Resource>()
                .Property(r => r.Name)
                .IsRequired()
                .IsUnicode()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50);

            modelBuilder.Entity<Resource>()
                .Property(r => r.Url)
                .IsRequired()
                .IsUnicode(false);

            modelBuilder.Entity<Resource>()
                .Property(r => r.ResourceType)
                .IsRequired(true);

            modelBuilder.Entity<Resource>()
                .Property(r => r.CourseId)
                .IsRequired();

            // One Course can have many Resources

            modelBuilder.Entity<Resource>()
                .HasOne(c => c.Course)
                .WithMany(c => c.Resources)
                .HasForeignKey(r => r.CourseId);

            /* Homework */

            modelBuilder.Entity<Homework>()
                .HasKey(h => h.HomeworkId);

            modelBuilder.Entity<Homework>()
                .Property(h => h.Content)
                .IsRequired()
                .IsUnicode(false);

            modelBuilder.Entity<Homework>()
                .Property(h => h.ContentType)
                .IsRequired()
                .IsUnicode();

            modelBuilder.Entity<Homework>()
                .Property(h => h.SubmissionTime)
                .IsRequired();

            modelBuilder.Entity<Homework>()
                .Property(h => h.StudentId)
                .IsRequired();

            modelBuilder.Entity<Homework>()
                .Property(h => h.CourseId)
                .IsRequired();

            // One Student can have many Homeworks
            modelBuilder.Entity<Homework>()
                .HasOne(h => h.Student)
                .WithMany(s => s.Homeworks)
                .HasForeignKey(h => h.StudentId);

            // One Course can have many Homeworks
            modelBuilder.Entity<Homework>()
                .HasOne(h => h.Course)
                .WithMany(c => c.Homeworks)
                .HasForeignKey(h => h.CourseId);

            /* StudentCourse */

            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new
                {
                    sc.StudentId, 
                    sc.CourseId
                });

            // One Student can have many Courses

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentsCourses)
                .HasForeignKey(sc => sc.StudentId);

            // One Course can have many Students

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentsCourses)
                .HasForeignKey(sc => sc.CourseId);


            base.OnModelCreating(modelBuilder);
        }



    }
}
