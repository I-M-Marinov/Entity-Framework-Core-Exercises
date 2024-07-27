using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Xml.Serialization;
using Trucks.Data.Models;
using Trucks.Data.Models.Enums;
using static Trucks.DataConstraints;

namespace Trucks.DataProcessor.ImportDto
{
    [XmlType(nameof(Truck))]
    public class ImportTruckDto
    {

        [Required]
        [XmlElement(nameof(RegistrationNumber))]
        [RegularExpression(TruckRegistrationNumberRegexValidation)]

        public string RegistrationNumber { get; set; } = null!;

        [StringLength(17, MinimumLength = 17)]
        [Required]
        [XmlElement(nameof(VinNumber))]
        public string VinNumber { get; set; } = null!;

        [Required]
        [Range(TruckTankCapacityMinValue, TruckTankCapacityMaxValue)]
        [XmlElement(nameof(TankCapacity))]

        public int TankCapacity { get; set; }

        [Required]
        [Range(TruckCargoCapacityMinValue, TruckCargoCapacityMaxValue)]
        [XmlElement(nameof(CargoCapacity))]

        public int CargoCapacity { get; set; }

        [Required]
        [XmlElement(nameof(CategoryType))]
        public int  CategoryType { get; set; }

        [Required]
        [XmlElement(nameof(MakeType))]
        public int MakeType { get; set; }

    }
}
