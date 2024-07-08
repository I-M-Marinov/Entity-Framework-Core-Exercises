using Microsoft.EntityFrameworkCore;
using ProductShop.Models;
namespace ProductShop.Data
{
    public class ProductShopContext : DbContext
    {
        public ProductShopContext()
        {
        }

        public ProductShopContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<CategoryProduct> CategoriesProducts { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryProduct>(entity =>
            {
                entity.HasKey(x => new { x.CategoryId, x.ProductId });
            });

            modelBuilder.Entity<User>(entity =>
            {
                modelBuilder.Entity<CategoryProduct>()
                    .HasKey(cp => new { cp.CategoryId, cp.ProductId });

                modelBuilder.Entity<User>(entity =>
                {
                    entity.HasMany(u => u.ProductsBought)
                        .WithOne(p => p.Buyer)
                        .HasForeignKey(p => p.BuyerId)
                        .OnDelete(DeleteBehavior.Restrict); // or DeleteBehavior.SetNull

                    entity.HasMany(u => u.ProductsSold)
                        .WithOne(p => p.Seller)
                        .HasForeignKey(p => p.SellerId)
                        .OnDelete(DeleteBehavior.Restrict); // or DeleteBehavior.SetNull
                });
            });

        }
    }
}
