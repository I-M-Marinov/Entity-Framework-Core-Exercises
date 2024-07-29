using Artillery.Data.Models.Enums;
using Artillery.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Artillery.DataProcessor.ExportDto
{
    [XmlType(nameof(Gun))]

    public class ExportGunsXmlDto
    {

        [XmlAttribute(nameof(Manufacturer))] 
        public string Manufacturer { get; set; } = null!;

        [XmlAttribute(nameof(GunType))]
        public string GunType { get; set; } = null!;

        [XmlAttribute(nameof(GunWeight))]
        public int GunWeight { get; set; }

        [XmlAttribute(nameof(BarrelLength))]
        public double BarrelLength { get; set; }

        [XmlAttribute(nameof(Range))]
        public int Range { get; set; }

        [XmlArray(nameof(Countries))]
        [XmlArrayItem(nameof(Country))]

        public ExportCountryDto[] Countries { get; set; } = new ExportCountryDto[0];
    }
}
