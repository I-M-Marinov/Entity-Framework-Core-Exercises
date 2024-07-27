
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Trucks.Data.Models;
using static Trucks.DataConstraints;


namespace Trucks.DataProcessor.ImportDto
{
    [XmlType(nameof(Despatcher))]
    public class ImportDispatcherDto
    {
        [XmlElement(nameof(Name))]
        [Required]
        [MinLength(DispatcherNameMinLength)]
        [MaxLength(DispatcherNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [XmlElement(nameof(Position))]
        public string Position { get; set; } = null!;


        [XmlArray(nameof(Trucks))]
        [XmlArrayItem("Truck")]
        public ImportTruckDto[] Trucks { get; set; }


    }
}
