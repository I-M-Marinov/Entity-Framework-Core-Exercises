
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Footballers.Data.Models;
using static Footballers.DataConstraints;

namespace Footballers.DataProcessor.ImportDto
{
    [XmlType(nameof(Coach))]
    public class ImportCoachesDto
    {

        [XmlElement(nameof(Name))]
        [Required]
        [MinLength(CoachNameMinLength)]
        [MaxLength(CoachNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [XmlElement(nameof(Nationality))]
        public string Nationality { get; set; } = null!;


        [XmlArray(nameof(Footballers))]
        public ImportFootballerDto[] Footballers { get; set; }

        
    }
}
