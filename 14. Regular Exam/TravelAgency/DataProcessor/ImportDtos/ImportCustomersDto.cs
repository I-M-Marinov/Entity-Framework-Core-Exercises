
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using TravelAgency.Data.Models;
using static TravelAgency.DataConstraints;

namespace TravelAgency.DataProcessor.ImportDtos
{
    [XmlType(nameof(Customer))]
    public class ImportCustomersDto
    {
        [Required]
        [MinLength(CustomerFullNameMinLength)]
        [MaxLength(CustomerFullNameMaxLength)]
        [XmlElement(nameof(FullName))]
        public string FullName { get; set; } = null!;

        [Required]
        [MinLength(CustomerEmailMinLength)]
        [MaxLength(CustomerEmailMaxLength)]
        [EmailAddress]
        [XmlElement(nameof(Email))]
        public string Email { get; set; } = null!;

        [Required]
        [RegularExpression(CustomerPhoneNumberRegexValidation)]
        [XmlAttribute("phoneNumber")]
        public string PhoneNumber { get; set; } = null!;
    }
}
