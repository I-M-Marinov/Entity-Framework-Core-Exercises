
using System.ComponentModel.DataAnnotations;
using Medicines.Data.Models;
using Newtonsoft.Json;
using static Medicines.DataConstraints.DataConstraints;

namespace Medicines.DataProcessor.ImportDtos
{
    public class ImportPatientDto
    {
        [JsonProperty("FullName")]
        [Required]
        [MinLength(PatientFullNameMinLength)]
        [MaxLength(PatientFullNameMaxLength)]
        public string FullName { get; set; } = null!;

        [JsonProperty("AgeGroup")]
        [Required]
        [Range(AgeGroupMinValue,AgeGroupMaxValue)]
        public int AgeGroup { get; set; }

        [JsonProperty("Gender")]
        [Required]
        [Range(GenderMinValue, GenderMaxValue)]
        public int Gender { get; set; }

        [JsonProperty("Medicines")]

        public int[] Medicines { get; set; }

    }
}
