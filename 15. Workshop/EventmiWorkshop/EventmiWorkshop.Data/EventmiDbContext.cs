using EventmiWorkshop.Data.Models;
using EventmiWorkshopMVC.Common;
using Microsoft.EntityFrameworkCore;

namespace EventmiWorkshop.Data
{
    public class EventmiDbContext: DbContext
    {


        public EventmiDbContext()
        {
            
        }

        public EventmiDbContext(DbContextOptions options)
            :base(options)
        {
            
        }

        public DbSet<Event> Events { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .Property(e => e.IsActive)
                .HasDefaultValue(true);
        }
    }
}
