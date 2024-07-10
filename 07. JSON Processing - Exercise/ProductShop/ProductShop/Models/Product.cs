namespace ProductShop.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product
    {
        public Product()
        {
            CategoriesProducts = new List<CategoryProduct>();
        }

        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public int SellerId { get; set; }

        [ForeignKey(nameof(SellerId))]
        [InverseProperty("ProductsSold")]
        public User Seller { get; set; } = null!;

        public int? BuyerId { get; set; }

        [ForeignKey(nameof(BuyerId))]
        [InverseProperty("ProductsBought")]
        public User? Buyer { get; set; }

        public ICollection<CategoryProduct> CategoriesProducts { get; set; }
    }
}