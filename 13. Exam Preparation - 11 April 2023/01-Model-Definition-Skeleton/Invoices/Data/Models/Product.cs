
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
         // [Range(typeof(decimal),ProductPriceMinValue,ProductPriceMaxValue)]
        public decimal Price { get; set; }

        [Required]
        public CategoryType CategoryType { get; set; }

        //[Required] 
        //public List<ProductClient> ProductClients { get; set; }

        // TODO: Add navigation property

    }
}
