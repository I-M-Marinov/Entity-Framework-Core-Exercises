
using System.ComponentModel.DataAnnotations;
using Trucks.Data.Models.Enums;
using static Trucks.DataConstraints;


namespace Trucks.DataProcessor.ExportDto
{
    public class ExportTrucksDto
    {
        [Required]
        public string TruckRegistrationNumber { get; set; } = null!;

        [StringLength(17, MinimumLength = 17)]
        [Required]
        public string VinNumber { get; set; } = null!;

        [Required]
        [Range(TruckTankCapacityMinValue,TruckTankCapacityMaxValue)]
        public int TankCapacity { get; set; }

        [Required]
        [Range(TruckCargoCapacityMinValue, TruckCargoCapacityMaxValue)]
        public int CargoCapacity { get; set; }

        [Required]
        public string CategoryType { get; set; }

        [Required]
        public string MakeType { get; set; }
    }
}
