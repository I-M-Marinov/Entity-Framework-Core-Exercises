
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Cadastre.Data.Models;
using static Cadastre.DataConstraints;


namespace Cadastre.DataProcessor.ImportDtos
{

    [XmlType(nameof(District))]

    public class ImportDistrictDto
    {
        [XmlAttribute(nameof(Region))]
        [Required]
        public string Region { get; set; } = null!;


        [XmlElement(nameof(Name))]
        [Required]
        [MinLength(DistrictNameMinLength)]
        [MaxLength(DistrictNameMaxLength)]
        public string Name { get; set; } = null!;

        [XmlElement(nameof(PostalCode))]
        [Required]
        [RegularExpression(DistrictPostalCodeRegexValidation)]
        public string PostalCode { get; set; } = null!;

        [XmlArray(nameof(Properties))]
        [XmlArrayItem("Property")]
        public ImportPropertyDto[] Properties { get; set; }

    }
}
