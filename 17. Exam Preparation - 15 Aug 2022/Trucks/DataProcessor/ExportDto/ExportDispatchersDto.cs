
using System.Xml.Serialization;
using Trucks.Data.Models;
using static Trucks.DataConstraints;

namespace Trucks.DataProcessor.ExportDto
{
    [XmlType(nameof(Despatcher))]
    public class ExportDispatchersDto
    {
        [XmlElement(nameof(DespatcherName))] 
        public string DespatcherName { get; set; } = null!;

        [XmlAttribute(nameof(TrucksCount))] 
        public int TrucksCount { get; set; }

        [XmlArray(nameof(Trucks))]
        [XmlArrayItem("Truck")]

        public ExportTruckXmlDto[] Trucks { get; set; }


        [XmlIgnore] 
        public ClientTruck ClientTruck { get; set; } = null!;
    }
}
