
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Footballers.Data.Models;
using Footballers.Data.Models.Enums;
using static Footballers.DataConstraints;

namespace Footballers.DataProcessor.ImportDto
{
    [XmlType(nameof(Footballer))]
    public class ImportFootballerDto
    {
        [XmlElement(nameof(Name))]
        [Required]
        [MinLength(FootballerNameMinLength)]
        [MaxLength(FootballerNameMaxLength)]
        public string Name { get; set; } = null!;

        [XmlElement(nameof(ContractStartDate))]
        [Required]
        public string ContractStartDate { get; set; } = null!;

        [XmlElement(nameof(ContractEndDate))]
        [Required]
        public string ContractEndDate { get; set; } = null!;

        [XmlElement(nameof(BestSkillType))]
        [Required]
        [Range(FootballerBestSkillTypeMinValue,FootballerBestSkillTypeMaxValue)]
        public int BestSkillType { get; set; }

        [XmlElement(nameof(PositionType))]
        [Required]
        [Range(FootballerPositionTypeMinValue, FootballerPositionTypeMaxValue)]
        public int PositionType { get; set; } 

    }
}
