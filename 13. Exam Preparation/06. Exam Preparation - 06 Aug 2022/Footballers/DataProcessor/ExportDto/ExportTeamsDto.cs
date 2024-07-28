using Footballers.Data.Models;
using System.ComponentModel.DataAnnotations;


namespace Footballers.DataProcessor.ExportDto
{
    public class ExportTeamsDto
    {
        [Required] 
        public string Name { get; set; } = null!;

        [Required]
        public ICollection<ExportFootballerDto> Footballers { get; set; } = null!;

    }
}
