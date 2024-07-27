using System.ComponentModel.DataAnnotations;
using static Trucks.DataConstraints;

namespace Trucks.Data.Models
{
    public class Despatcher
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(DispatcherNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        public string Position { get; set; } = null!;

        public virtual ICollection<Truck> Trucks { get; set; } = new HashSet<Truck>();
    }
}
