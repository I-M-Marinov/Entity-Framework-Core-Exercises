using System.ComponentModel.DataAnnotations;
using Trucks.Data.Models;
using static Trucks.DataConstraints;

namespace Trucks.DataProcessor.ImportDto
{
    public class ImportClientDto
    {
        [Required]
        [MinLength(ClientNameMinLength)]
        [MaxLength(ClientNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MinLength(ClientNationalityMinLength)]
        [MaxLength(ClientNationalityMaxLength)]
        public string Nationality { get; set; } = null!;

        [Required]
        public string Type { get; set; } = null!;

        public int[] Trucks { get; set; }
    }
}
