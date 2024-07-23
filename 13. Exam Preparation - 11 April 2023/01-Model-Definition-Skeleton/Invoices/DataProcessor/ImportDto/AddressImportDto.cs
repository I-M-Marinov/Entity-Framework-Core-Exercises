
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Invoices.Data.Models;
using static Invoices.Data.Models.DataConstraints;

namespace Invoices.DataProcessor.ImportDto
{
    [XmlType(nameof(Address))]
    public class AddressImportDto
    {

        [XmlElement(nameof(StreetName))] 
        public string StreetName { get; set; } = null!;


        [XmlElement(nameof(StreetNumber))]
        [Required]
        [MinLength(AddressStreetNameMinLength)]
        [MaxLength(AddressStreetNameMaxLength)]
        public int StreetNumber { get; set; }

        [XmlElement(nameof(PostCode))]
        public string PostCode { get; set; } = null!;

        [XmlElement(nameof(City))]
        [Required]
        [MinLength(AddressCityMinLength)]
        [MaxLength(AddressCityMaxLength)]
        public string City  { get; set; } = null!;

        [XmlElement(nameof(Country))]
        public string Country { get; set; } = null!;
    }
}
