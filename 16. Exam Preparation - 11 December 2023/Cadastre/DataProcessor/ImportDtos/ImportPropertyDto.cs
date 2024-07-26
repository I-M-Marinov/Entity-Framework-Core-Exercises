using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Xml.Serialization;
using Cadastre.Data.Models;
using static Cadastre.DataConstraints;

namespace Cadastre.DataProcessor.ImportDtos
{
    [XmlType(nameof(Property))]

    public class ImportPropertyDto
    {
        [XmlElement(nameof(PropertyIdentifier))]
        [Required]
        [MinLength(PropertyIdentifierMinLength)]
        [MaxLength(PropertyIdentifierMaxLength)]
        public string PropertyIdentifier { get; set; } = null!;

        [XmlElement(nameof(Area))]
        [Required]
        public int Area { get; set; }

        [XmlElement(nameof(Details))]
        [MinLength(PropertyDetailsMinLength)]       
        [MaxLength(PropertyDetailsMaxLength)]
        public string? Details { get; set; }

        [Required]
        [XmlElement(nameof(Address))]
        [MinLength(PropertyAddressMinLength)]
        [MaxLength(PropertyAddressMaxLength)]
        public string Address { get; set; } = null!;

        [Required]
        [XmlElement(nameof(DateOfAcquisition))]
        public string DateOfAcquisition { get; set; } = null!;
    }
}
