
using Medicines.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using static Medicines.DataConstraints.DataConstraints;


namespace Medicines.DataProcessor.ImportDtos
{
    [XmlType(nameof(Pharmacy))]
    public class ImportPharmacyDto
    {
        [XmlElement(nameof(Name))]
        [Required]
        [MinLength(PharmacyNameMinLength)]
        [MaxLength(PharmacyNameMaxLength)]
        public string Name { get; set; } = null!;


        [XmlElement(nameof(PhoneNumber))]
        [Required]
        [RegularExpression(PharmacyPhoneNumberRegexValidation)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [XmlAttribute("non-stop")]
        [RegularExpression(PharmacyBooleanRegexValidation)]
        public string IsNonStop { get; set; } = null!;

        [XmlArray(nameof(Medicines))]
        [XmlArrayItem("Medicine")]
        public ImportMedicineDto[] Medicines { get; set; }

    }
}
