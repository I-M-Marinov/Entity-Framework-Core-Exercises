using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Medicines.Data.Models;

namespace Medicines.DataProcessor.ExportDtos
{
    [XmlType(nameof(Medicine))]
    public class ExportMedicineXmlDto
    {


        [XmlElement(nameof(Name))]
        public string Name { get; set; } = null!;

        [XmlElement(nameof(Price))]
        public string Price { get; set; } = null!;

        [XmlAttribute(nameof(Category))]
        public string Category { get; set; } = null!;

        [XmlElement(nameof(Producer))]
        public string Producer { get; set; } = null!;

        [XmlElement("BestBefore")]
        public string ExpiryDate { get; set;} = null!;

    }
}
