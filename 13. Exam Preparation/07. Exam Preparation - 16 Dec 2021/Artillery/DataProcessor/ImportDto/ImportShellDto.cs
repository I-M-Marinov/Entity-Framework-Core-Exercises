using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Artillery.Data.Models;
using static Artillery.DataConstraints;

namespace Artillery.DataProcessor.ImportDto
{
    [XmlType(nameof(Shell))]
    public class ImportShellDto
    {
        [Required]
        [Range(ShellWeightMinValue, ShellWeightMaxValue)]
        [XmlElement(nameof(ShellWeight))]

        public double ShellWeight { get; set; }

        [Required]
        [MinLength(ShellCaliberMinLength)]
        [MaxLength(ShellCaliberMaxLength)]
        [XmlElement(nameof(Caliber))]

        public string Caliber { get; set; } = null!;


    }
}
