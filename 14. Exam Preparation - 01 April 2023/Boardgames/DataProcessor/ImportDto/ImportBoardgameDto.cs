
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Boardgames.Data.Models;
using Boardgames.Data.Models.Enums;
using static Boardgames.Data.DataConstraints;


namespace Boardgames.DataProcessor.ImportDto
{
    [XmlType(nameof(Boardgame))]

    public class ImportBoardgameDto
    {

        [XmlElement(nameof(Name))]
        [Required]
        [MinLength(BoardGameNameMinLength)]
        [MaxLength(BoardGameNameMaxLength)]
        public string Name { get; set; } = null!;


        [XmlElement(nameof(Rating))]
        [Required]
        public double Rating { get; set; }

        [XmlElement(nameof(YearPublished))]
        [Required]
        [Range(BoardGameYearPublishedMinValue,BoardGameYearPublishedMaxValue)]
        public int YearPublished { get; set; }

        [XmlElement(nameof(CategoryType))]
        [Range(CategoryTypeMinValue, CategoryTypeMaxValue)]
        [Required]
        public int CategoryType { get; set; }

        [XmlElement(nameof(Mechanics))]
        [Required]
        public string Mechanics { get; set; } = null!;

    }
}
