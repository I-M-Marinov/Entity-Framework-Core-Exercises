
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Invoices.Data.Models;
using Newtonsoft.Json;
using static Invoices.Data.Models.DataConstraints;

namespace Invoices.DataProcessor.ImportDto
{
    [XmlType(nameof(Address))]
    public class ImportAddressDTO
    {

        [XmlElement(nameof(StreetName))]
        [Required]
        [MinLength(AddressStreetNameMinLength)]
        [MaxLength(AddressStreetNameMaxLength)]
        public string StreetName { get; set; } = null!;


        [XmlElement(nameof(StreetNumber))]
        [Required]

        public int StreetNumber { get; set; }

        [Required]
        [XmlElement(nameof(PostCode))]
        public string PostCode { get; set; } = null!;

        [XmlElement(nameof(City))]
        [Required]
        [MinLength(AddressCityMinLength)]
        [MaxLength(AddressCityMaxLength)]
        public string City { get; set; } = null!;

        [Required]
        [XmlElement(nameof(Country))]
        [MinLength(AddressCountryMinLength)]
        [MaxLength(AddressCountryMaxLength)]
        public string Country { get; set; } = null!;
    }
}
