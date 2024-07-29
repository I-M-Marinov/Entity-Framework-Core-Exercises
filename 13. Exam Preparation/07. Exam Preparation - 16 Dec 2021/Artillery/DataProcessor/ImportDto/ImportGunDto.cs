using Artillery.Data.Models.Enums;
using Artillery.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using static Artillery.DataConstraints;

namespace Artillery.DataProcessor.ImportDto
{
    public class ImportGunDto
    {
        [Required]
        public int ManufacturerId { get; set; }

        [Required]
        [Range(GunWeightMinValue,GunWeightMaxValue)]
        public int GunWeight { get; set; }

        [Required]
        [Range(GunBarrelLengthMinValue, GunBarrelLengthMaxValue)]
        public double BarrelLength { get; set; }

        public int? NumberBuild { get; set; }

        [Required]
        [Range(GunRangeMinValue, GunRangeMaxValue)]
        public int Range { get; set; }

        [Required] 
        public string GunType { get; set; } = null!;

        [Required]
        public int ShellId { get; set; }

        public ImportCountriesDto[] Countries { get; set; } = Array.Empty<ImportCountriesDto>();
    }
}
