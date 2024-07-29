using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Artillery.DataConstraints;

namespace Artillery.Data.Models
{
    public class CountryGun
    {
        [Required]
        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }

        [Required]
        public virtual Country Country { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Gun))]
        public int GunId { get; set; }

        [Required]
        public virtual Gun Gun { get; set; } = null!;
    }
}
