using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Cadastre.Data.Models;

namespace Cadastre.DataProcessor.ExportDtos
{
    [XmlType(nameof(Property))]
    public class ExportPropertyXmlDto
    {

        [XmlAttribute("postal-code")] 
        public string PostalCode { get; set; } = null!;

        [XmlElement(nameof(PropertyIdentifier))] 
        public string PropertyIdentifier { get; set; } = null!;

        [XmlElement(nameof(Area))] 
        public string Area { get; set; } = null!;

        [XmlElement(nameof(DateOfAcquisition))]
        public string DateOfAcquisition { get; set; } = null!;
    }
}
