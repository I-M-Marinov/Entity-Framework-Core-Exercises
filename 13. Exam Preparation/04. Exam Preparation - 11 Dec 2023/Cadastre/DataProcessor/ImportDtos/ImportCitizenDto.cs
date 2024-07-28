using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Cadastre.Data.Models;
using static Cadastre.DataConstraints;

namespace Cadastre.DataProcessor.ImportDtos
{
    public class ImportCitizenDto
    {
        [Required]
        [MinLength(CitizenFirstNameMinLength)]
        [MaxLength(CitizenFirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MinLength(CitizenLastNameMinLength)]
        [MaxLength(CitizenLastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [Required]
        public string BirthDate { get; set; } = null!;

        [Required]
        public string MaritalStatus { get; set; } = null!;

        [Required]
        public int[] Properties { get; set; }
     }
}
