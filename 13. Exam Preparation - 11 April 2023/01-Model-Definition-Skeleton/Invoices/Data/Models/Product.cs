
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Invoices.Data.Models.Enums;
using static Invoices.Data.Models.DataConstraints;


namespace Invoices.Data.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ProductNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        // [Range(typeof(decimal),ProductPriceMinValue,ProductPriceMaxValue)] to be used in the DTO for validation not here  

        public decimal Price { get; set; }

        [Required]
        public CategoryType CategoryType { get; set; }


        public virtual ICollection<ProductClient> ProductsClients { get; set; } = new HashSet<ProductClient>();


    }
}
