
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Artillery.Data.Models;
using static Artillery.DataConstraints;

namespace Artillery.DataProcessor.ImportDto
{
    [XmlType(nameof(Country))]
    public class ImportCountryDto
    {
        [XmlElement(nameof(CountryName))]
        [MinLength(CountryNameMinLength)]
        [MaxLength(CountryNameMaxLength)]
        [Required]
        public string CountryName { get; set; } = null!;

        [Required]
        [XmlElement(nameof(ArmySize))]
        [Range(CountryArmySizeMinValue,CountryArmySizeMaxValue)]
        public int ArmySize { get; set; }

    }
}
