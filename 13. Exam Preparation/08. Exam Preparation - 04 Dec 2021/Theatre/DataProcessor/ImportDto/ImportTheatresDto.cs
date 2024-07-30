using System.ComponentModel.DataAnnotations;
using static Theatre.DataConstraints;


namespace Theatre.DataProcessor.ImportDto
{
    public class ImportTheatresDto
    {
        [Required]
        [MinLength(TheatreNameMinLength)]
        [MaxLength(TheatreNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [Range(TheatreNumberOfHallsMinValue,TheatreNumberOfHallsMaxValue)]
        public int NumberOfHalls { get; set; }

        [Required]
        [MinLength(TheatreDirectorMinLength)]
        [MaxLength(TheatreDirectorMaxLength)]
        public string Director { get; set; } = null!;

        public ICollection<ImportTicketsDto> Tickets { get; set; }
    }
}
