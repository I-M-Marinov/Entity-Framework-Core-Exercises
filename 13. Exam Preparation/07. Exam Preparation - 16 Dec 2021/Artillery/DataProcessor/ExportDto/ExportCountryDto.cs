using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Artillery.Data.Models;

namespace Artillery.DataProcessor.ExportDto
{
    [XmlType(nameof(Country))]
    public class ExportCountryDto
    {
        [XmlAttribute(nameof(Country))]
        public string Country { get; set; } = null!;

        [XmlAttribute(nameof(ArmySize))]
        public int ArmySize { get; set; }
    }
}
