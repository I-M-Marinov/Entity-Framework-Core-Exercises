using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static Artillery.DataConstraints;


namespace Artillery.Data.Models
{
    public class Manufacturer
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(ManufacturerNameMaxLength)]
        public string ManufacturerName { get; set; } = null!;

        [Required]
        [MaxLength(ManufacturerFoundedMaxLength)]
        public string Founded { get; set; } = null!;

        public virtual ICollection<Gun> Guns { get; set; } = new HashSet<Gun>();
    }
}
