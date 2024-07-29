
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Artillery.Data.Models;
using static Artillery.DataConstraints;

namespace Artillery.DataProcessor.ImportDto
{
    [XmlType(nameof(Manufacturer))]
    public class ImportManufacturerDto
    {
        [Required]
        [MinLength(ManufacturerNameMinLength)]
        [MaxLength(ManufacturerNameMaxLength)]
        [XmlElement(nameof(ManufacturerName))]
        public string ManufacturerName { get; set; } = null!;

        [Required]
        [MinLength(ManufacturerFoundedMinLength)]
        [MaxLength(ManufacturerFoundedMaxLength)]
        [XmlElement(nameof(Founded))]
        public string Founded { get; set; } = null!;
    }
}
