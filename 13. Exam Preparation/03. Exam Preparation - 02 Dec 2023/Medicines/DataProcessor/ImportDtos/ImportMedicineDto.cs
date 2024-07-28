
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Medicines.Data.Models;
using static Medicines.DataConstraints.DataConstraints;


namespace Medicines.DataProcessor.ImportDtos
{
    [XmlType(nameof(Medicine))]
    public class ImportMedicineDto
    {

        [XmlElement(nameof(Name))]
        [Required]
        [MinLength(MedicineNameMinLength)]
        [MaxLength(MedicineNameMaxLength)]
        public string Name { get; set; } = null!;


        [XmlElement(nameof(Price))]
        [Range(MedicinePriceMinValue,MedicinePriceMaxValue)]
        [Required]

        public double Price { get; set; }

        [Required]
        [XmlElement(nameof(ProductionDate))]
        public string ProductionDate { get; set; } = null!;

        [XmlElement(nameof(ExpiryDate))]
        [Required]

        public string ExpiryDate { get; set; } = null!;

        [Required]
        [XmlElement(nameof(Producer))]
        [MinLength(MedicineProducerMinLength)]
        [MaxLength(MedicineProducerMaxLength)]
        public string Producer { get; set; } = null!;

        [Required]
        [XmlAttribute("category")]
        [Range(MedicineCategoryMinValue, MedicineCategoryMaxValue)]

        public int Category { get; set; }
    }
}
