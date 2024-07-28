using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Footballers.Data.Models;

namespace Footballers.DataProcessor.ExportDto
{
    [XmlType(nameof(Footballer))]
    public class ExportFootballerXmlDto
    {
        [XmlElement(nameof(Name))] 
        public string Name { get; set; } = null!;

        [XmlElement(nameof(Position))]
        public string Position { get; set; } = null!;
    }
}
