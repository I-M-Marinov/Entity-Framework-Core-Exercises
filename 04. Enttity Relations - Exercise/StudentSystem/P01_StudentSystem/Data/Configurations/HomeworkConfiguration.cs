using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01_StudentSystem.Data.Configurations
{
    public class HomeworkConfiguration : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> homeworkEntity)
        {
            homeworkEntity
                .HasKey(h => h.HomeworkId);

            homeworkEntity
                 .Property(h => h.Content)
                .IsRequired()
                .IsUnicode(false);

            homeworkEntity
                 .Property(h => h.ContentType)
                .IsRequired()
                .IsUnicode();

            homeworkEntity
                .Property(h => h.SubmissionTime)
                .IsRequired();

            homeworkEntity
                .Property(h => h.StudentId)
                .IsRequired();

            homeworkEntity
                .Property(h => h.CourseId)
                .IsRequired();

            // One Student can have many Homeworks

            homeworkEntity
                .HasOne(h => h.Student) // One Student
                .WithMany(s => s.Homeworks) // Many Homeworks
                .HasForeignKey(h => h.StudentId);

            // One Course can have many Homeworks

            homeworkEntity
                .HasOne(h => h.Course) // One Course
                .WithMany(c => c.Homeworks) // Many Homeworks
                .HasForeignKey(h => h.CourseId);
        }
    }
}
