using Medicines.Data.Models;
using Medicines.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;


namespace Medicines.DataProcessor.ExportDtos
{
    [XmlType(nameof(Patient))]

    public class ExportPatientDto
    {


        [XmlElement("Name")]
        public string FullName { get; set; } = null!;

        [XmlElement(nameof(AgeGroup))]
        public string AgeGroup { get; set; } = null!;

        [XmlAttribute(nameof(Gender))]
        public string Gender { get; set; } = null!;

        [XmlArray(nameof(Medicines))]
        [XmlArrayItem(nameof(Medicine))]
        public ExportMedicineXmlDto[] Medicines { get; set; } = new ExportMedicineXmlDto[0];

    }
}
