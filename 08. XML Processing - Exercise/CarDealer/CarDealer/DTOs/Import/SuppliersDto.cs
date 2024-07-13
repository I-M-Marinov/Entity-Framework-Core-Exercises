using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.DTOs.Import
{
    using System.Xml.Serialization;

    [XmlType("Supplier")]
    public class SuppliersDto
    {
        [XmlElement("name")] 
        public string Name { get; set; } = null!;

        [XmlElement("isImporter")] 
        public bool IsImporter { get; set; }
    }
}
