using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using VaporStore.Data.Models;

namespace VaporStore.DataProcessor.ExportDto
{
    [XmlType(nameof(Game))]
    public class ExportGameXmlDto
    {
        [XmlAttribute("title")]
        public string Title { get; set; }

        [XmlElement(nameof(Genre))]
        public string Genre { get; set; }

        [XmlElement(nameof(Price))]
        public string Price { get; set; }


    }
}
