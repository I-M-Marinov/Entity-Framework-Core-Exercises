using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BlogDemo
{
    public class BlogDbContext : DbContext
    {

        public BlogDbContext()
        {

        }

        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                .HasKey(b => b.BlogId);

            modelBuilder.Entity<Blog>()
                .ToTable("Blogs", "blg");

            modelBuilder.Entity<Blog>()
                .Property(b => b.Name)
                .HasColumnName("BlogName")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Blog>()
                .Property(b => b.Description)
                .HasColumnName("Description")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(500);

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
    }
}
