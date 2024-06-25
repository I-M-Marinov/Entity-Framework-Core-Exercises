using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;

namespace P01_StudentSystem.Data.Configurations
{
    public class StudentCourseConfiguration : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> studentCourse)
        {

            studentCourse
                .HasKey(sc => new
                {
                    sc.StudentId,
                    sc.CourseId
                });

            // One Student can have many Courses

            studentCourse
                .HasOne(sc => sc.Student) // One Student
                .WithMany(s => s.StudentsCourses) // Many Courses
                .HasForeignKey(sc => sc.StudentId);

            // One Course can have many Students

            studentCourse
                .HasOne(sc => sc.Course) // One Course
                .WithMany(c => c.StudentsCourses) // Many Students
                .HasForeignKey(sc => sc.CourseId);
        }
    }
}
