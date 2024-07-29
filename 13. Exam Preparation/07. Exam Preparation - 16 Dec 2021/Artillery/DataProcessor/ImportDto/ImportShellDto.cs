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
        public double ShellWeight { get; set; }

        [Required]
        [MinLength(ShellCaliberMinLength)]
        [MaxLength(ShellCaliberMaxLength)]
        public string Caliber { get; set; } = null!;


    }
}
