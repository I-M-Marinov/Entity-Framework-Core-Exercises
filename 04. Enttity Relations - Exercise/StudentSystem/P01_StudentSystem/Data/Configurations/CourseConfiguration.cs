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
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> courseEntity)
        {
            courseEntity
             .HasKey(c => c.CourseId);

            courseEntity
                 .Property(c => c.Name)
                .IsRequired()
                .IsUnicode()
            .HasMaxLength(80);

            courseEntity
                 .Property(c => c.Description)
                .IsUnicode()
                .IsRequired(false);

            courseEntity
                .Property(c => c.StartDate)
            .IsRequired();

            courseEntity
                .Property(c => c.EndDate)
                .IsRequired();

            courseEntity
                .Property(c => c.Price)
                .IsRequired();
        }
    }
}
