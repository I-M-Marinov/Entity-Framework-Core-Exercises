using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Theatre.Data.Models;

namespace Theatre.DataProcessor.ExportDto
{
    [XmlType("Actor")]

    public class ExportActorDto
    {
        [XmlAttribute(nameof(FullName))] 
        public string FullName { get; set; } = null!;

        [XmlAttribute(nameof(MainCharacter))]
        public string MainCharacter { get; set; } = null!;
    }
}
