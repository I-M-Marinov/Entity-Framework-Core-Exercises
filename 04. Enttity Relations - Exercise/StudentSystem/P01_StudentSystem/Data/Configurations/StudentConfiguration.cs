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
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> studentEntity)
        {
            studentEntity
                .HasKey(s => s.StudentId);

            studentEntity
                .Property(s => s.Name)
                .HasMaxLength(100)
                .IsUnicode()
                .IsRequired();

            studentEntity
                 .Property(s => s.PhoneNumber)
                .HasColumnType("CHAR(10)")
                .IsFixedLength()
                .IsUnicode(false)
                .IsRequired(false); // default value for the IsFixedLength is TRUE, so we can be explicit or not 

            studentEntity
                .Property(s => s.RegisteredOn)
                .ValueGeneratedOnAdd()
                .IsRequired(); // default value for the IsRequired is TRUE, so we can be explicit or not 

            studentEntity
                .Property(s => s.Birthday)
                .IsRequired(false);
        }
    }
    
}
