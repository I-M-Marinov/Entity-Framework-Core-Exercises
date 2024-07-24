using Boardgames.Data.Models;
using System.ComponentModel.DataAnnotations;
using static Boardgames.Data.DataConstraints;
using System.Xml.Serialization;

namespace Boardgames.DataProcessor.ImportDto
{
    [XmlType(nameof(Creator))]

    public class ImportCreatorsDto
    {

        [XmlElement(nameof(FirstName))]
        [Required]
        [MinLength(CreatorFirstNameMinLength)]
        [MaxLength(CreatorFirstNameMaxLength)]

        public string FirstName { get; set; } = null!;

        [XmlElement(nameof(LastName))]
        [Required]
        [MinLength(CreatorLastNameMinLength)]
        [MaxLength(CreatorLastNameMaxLength)]

        public string LastName { get; set; } = null!;


        [XmlArray(nameof(Boardgames))] 
        public ImportBoardgameDto[] Boardgames { get; set; } = null!;
    }
}
