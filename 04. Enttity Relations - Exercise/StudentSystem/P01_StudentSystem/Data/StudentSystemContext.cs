using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Configurations;
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

            modelBuilder
                .ApplyConfiguration(new StudentConfiguration());

            /* Course */

            modelBuilder
                .ApplyConfiguration(new CourseConfiguration());

            /* Resource */

            modelBuilder
                .ApplyConfiguration(new ResourceConfiguration());

            /* Homework */

            modelBuilder
                .ApplyConfiguration(new HomeworkConfiguration());

            /* StudentCourse */

            modelBuilder
                .ApplyConfiguration(new StudentCourseConfiguration());



            base.OnModelCreating(modelBuilder);
        }


    }
}
