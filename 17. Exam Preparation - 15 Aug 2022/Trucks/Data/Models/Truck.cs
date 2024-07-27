using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trucks.Data.Models.Enums;
using static Trucks.DataConstraints;

namespace Trucks.Data.Models
{
    public class Truck
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required] 
        public string RegistrationNumber { get; set; } = null!;

        [StringLength(17, MinimumLength = 17)]
        [Required]
        public string VinNumber { get; set; } = null!;
        
        [Required]
        [MaxLength(TruckTankCapacityMaxValue)]
        public int TankCapacity { get; set; }

        [Required]
        [MaxLength(TruckCargoCapacityMaxValue)]
        public int CargoCapacity { get; set; }

        [Required]
        public CategoryType CategoryType { get; set; }

        [Required]
        public MakeType MakeType { get; set; }

        [Required]
        [ForeignKey(nameof(Despatcher))]
        public int DespatcherId { get; set; }

        [Required] 
        public virtual Despatcher Despatcher { get; set; } = null!;

        public virtual ICollection<ClientTruck> ClientsTrucks { get; set; } = new HashSet<ClientTruck>();
    }
}
