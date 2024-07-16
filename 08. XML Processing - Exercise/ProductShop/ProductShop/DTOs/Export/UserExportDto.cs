using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProductShop.DTOs.Export
{
    [XmlType("User")]
    public class UserExportDto
    {
        [XmlElement("firstName")]
        public string FirstName { get; set; }

        [XmlElement("lastName")]
        public string LastName { get; set; }

        [XmlIgnore]
        [XmlElement("age")] 
        public int? Age { get; set; }

       // [XmlIgnore] 
        [XmlArray("soldProducts")]
        public ProductInRangeExportDto[] SoldProducts { get; set; }

        [XmlElement("SoldProducts")]
        public SoldProductsExportDto Products { get; set; }
    }
}
