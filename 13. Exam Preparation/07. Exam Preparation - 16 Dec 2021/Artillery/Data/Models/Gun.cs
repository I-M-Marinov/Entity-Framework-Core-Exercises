
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Artillery.Data.Models.Enums;
using static Artillery.DataConstraints;

namespace Artillery.Data.Models
{
    public class Gun
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Manufacturer))]
        public int ManufacturerId { get; set; }

        [Required]
        public virtual Manufacturer Manufacturer { get; set; } = null!;

        [Required]
        [MaxLength(GunWeightMaxValue)]
        public int GunWeight { get; set; }

        [Required]
        [Range(GunBarrelLengthMinValue,GunBarrelLengthMaxValue)]
        public double BarrelLength { get; set; }

        public int? NumberBuild { get; set; }

        [Required]
        [MaxLength(GunRangeMaxValue)]
        public int Range { get; set; }

        [Required]
        public GunType GunType { get; set; }

        [Required]
        [ForeignKey(nameof(Shell))]
        public int ShellId { get; set; }

        [Required] 
        public virtual Shell Shell { get; set; } = null!;

        public virtual ICollection<CountryGun> CountriesGuns { get; set; } = new HashSet<CountryGun>();

    }
}
