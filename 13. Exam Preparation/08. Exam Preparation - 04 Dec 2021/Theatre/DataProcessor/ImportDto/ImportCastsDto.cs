
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Theatre.Data.Models;
using static Theatre.DataConstraints;

namespace Theatre.DataProcessor.ImportDto
{

    [XmlType(nameof(Cast))]
    public class ImportCastsDto
    {
        [XmlElement(nameof(FullName))]
        [Required]
        [MinLength(CastFullNameMinLength)]
        [MaxLength(CastFullNameMaxLength)]
        public string FullName { get; set; } = null!;

        [XmlElement(nameof(IsMainCharacter))]
        [Required]
        public string IsMainCharacter { get; set; } = null!;

        [XmlElement(nameof(PhoneNumber))]
        [Required]
        [RegularExpression(CastPhoneNumberRegexValidation)]
        public string PhoneNumber { get; set; } = null!;

        [XmlElement(nameof(PlayId))]
        [Required]
        public int PlayId { get; set; }
    }
}
