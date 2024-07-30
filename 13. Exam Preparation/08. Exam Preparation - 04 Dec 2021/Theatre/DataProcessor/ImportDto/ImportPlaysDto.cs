
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Theatre.Data.Models;
using Theatre.Data.Models.Enums;
using static Theatre.DataConstraints;


namespace Theatre.DataProcessor.ImportDto
{
    [XmlType(nameof(Play))]
    public class ImportPlaysDto
    {
        [XmlElement(nameof(Title))]
        [MinLength(PlayTitleMinLength)]
        [MaxLength(PlayTitleMaxLength)]
        public string Title { get; set; } = null!;

        [XmlElement(nameof(Duration))]
        [Required]
        [Range(typeof(TimeSpan), "01:00:00", "23:59:59")]
        public string Duration { get; set; } = null!;

        [XmlElement(nameof(Raiting))]
        [Required]
        [Range(PlayRatingMinLength, PlayRatingMaxLength)]

        public double Raiting { get; set; }

        [XmlElement(nameof(Genre))] 
        [Required] 
        public string Genre { get; set; } = null!;

        [XmlElement(nameof(Description))]
        [Required]
        [StringLength(PlayDescriptionMaxLength)]
        public string Description { get; set; } = null!;


        [XmlElement(nameof(Screenwriter))]
        [Required]
        [MaxLength(PlayScreenwriterMaxLength)]

        public string Screenwriter { get; set; } = null!;
    }
}
