using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cadastre.Data.Enumerations;
using static Cadastre.DataConstraints;

namespace Cadastre.Data.Models
{
    public class Property
    {
        [Required]
        [Key]

        public int Id { get; set; }

        [Required]
        [MaxLength(PropertyIdentifierMaxLength)]
        public string PropertyIdentifier { get; set; } = null!;

        [Required]
        public int Area { get; set; }

        [MaxLength(PropertyDetailsMaxLength)]
        public string? Details { get; set; }

        [Required]
        [MaxLength(PropertyAddressMaxLength)]

        public string Address { get; set; } = null!;

        [Required]
        
        public DateTime DateOfAcquisition { get; set; }

        [Required]
        [ForeignKey(nameof(District))]
        public int DistrictId { get; set; }

        [Required]
        public virtual District District { get; set; } = null!;

        public virtual ICollection<PropertyCitizen> PropertiesCitizens { get; set; } = new HashSet<PropertyCitizen>();



    }
}
