
using System.ComponentModel.DataAnnotations;
using static Artillery.DataConstraints;


namespace Artillery.Data.Models
{
    public class Country
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(CountryNameMaxLength)]
        public string CountryName { get; set; } = null!;

        [Required]
        [MaxLength(CountryArmySizeMaxValue)]
        public int ArmySize { get; set; }

        public virtual ICollection<CountryGun> CountriesGuns { get; set; } = new HashSet<CountryGun>();
    }
}
