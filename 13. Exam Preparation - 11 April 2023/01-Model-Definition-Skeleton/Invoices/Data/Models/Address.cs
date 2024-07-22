
using System.ComponentModel.DataAnnotations;
using static Invoices.Data.Models.DataConstraints;

namespace Invoices.Data.Models
{
    public  class Address
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(AddressStreetNameMaxLength)]
        public string StreetName { get; set; } = null!;

        [Required]
        public int StreetNumber { get; set; }

        [Required]
        public string PostCode { get; set; } = null!;

        [Required]
        [MaxLength(AddressCityMaxLength)]
        public string City { get; set; } = null!;


        [Required]
        [MaxLength(AddressCountryMaxLength)]
        public string Country { get; set; } = null!;

        // TODO: Add navigation property
    }
}
