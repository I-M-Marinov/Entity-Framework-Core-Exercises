using System.ComponentModel.DataAnnotations;
using Cadastre.Data.Enumerations;
using static Cadastre.DataConstraints;

namespace Cadastre.Data.Models
{
    public class District
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required] 
        [MaxLength(DistrictNameMaxLength)] 
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(DistrictPostalCodeMaxLength)]
        public string PostalCode { get; set; } = null!;

        [Required]
        public Region Region { get; set; }

        public virtual ICollection<Property> Properties { get; set; } = new HashSet<Property>();


    }
}
