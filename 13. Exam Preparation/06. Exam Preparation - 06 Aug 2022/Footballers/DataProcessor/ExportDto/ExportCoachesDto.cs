using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Footballers.Data.Models;

namespace Footballers.DataProcessor.ExportDto
{
    [XmlType(nameof(Coach))]

    public class ExportCoachesDto
    {
        [XmlElement(nameof(Name))] 
        public string Name { get; set; } = null!;

        [XmlAttribute(nameof(FootballersCount))]
        public int FootballersCount { get; set; }

        [XmlArray(nameof(Footballers))]
        [XmlArrayItem("Footballer")]

        public ExportFootballerXmlDto[] Footballers { get; set; }

    }
}
