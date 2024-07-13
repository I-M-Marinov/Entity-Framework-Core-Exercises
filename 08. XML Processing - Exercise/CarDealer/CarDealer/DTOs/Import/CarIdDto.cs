using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.DTOs.Import
{
    using System.Xml.Serialization;
    [XmlType("carId")]
    public class CarIdDto
    {
        [XmlAttribute("carId")]
        public int Id { get; set; }
    }
}