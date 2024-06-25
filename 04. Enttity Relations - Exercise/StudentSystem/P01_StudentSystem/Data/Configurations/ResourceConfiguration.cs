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
    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> resourceEntity)
        {
            resourceEntity
                .HasKey(r => r.ResourceId);

            resourceEntity
                .Property(r => r.Name)
                .IsRequired()
                .IsUnicode()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50);

            resourceEntity
                .Property(r => r.Url)
                .IsRequired()
                .IsUnicode(false);

            resourceEntity
                .Property(r => r.ResourceType)
                .IsRequired(true);

            resourceEntity
                .Property(r => r.CourseId)
                .IsRequired();

            // One Course can have many Resources

            resourceEntity
                .HasOne(c => c.Course) // One Course
                .WithMany(c => c.Resources) // Many Resources 
                .HasForeignKey(r => r.CourseId);
        }
    }
}
