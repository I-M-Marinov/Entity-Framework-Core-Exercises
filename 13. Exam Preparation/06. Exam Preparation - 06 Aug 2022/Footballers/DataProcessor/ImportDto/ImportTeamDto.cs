
using System.ComponentModel.DataAnnotations;
using static Footballers.DataConstraints;

namespace Footballers.DataProcessor.ImportDto
{
    public class ImportTeamDto
    {
        [Required]
        [MinLength(TeamNameMinLength)]
        [MaxLength(TeamNameMaxLength)]
        [RegularExpression(TeamNameRegexValidation)]

        public string Name { get; set; } = null!;

        [Required]
        [MinLength(TeamNationalityMinLength)]
        [MaxLength(TeamNationalityMaxLength)]
        public string Nationality { get; set; } = null!;

        [Required]
        public int Trophies { get; set; }

        public int[] Footballers { get; set; }

    }
}
