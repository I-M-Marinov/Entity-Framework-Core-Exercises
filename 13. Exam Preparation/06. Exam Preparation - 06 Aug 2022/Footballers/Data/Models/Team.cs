
using System.ComponentModel.DataAnnotations;
using static Footballers.DataConstraints;


namespace Footballers.Data.Models
{
    public class Team
    {

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(TeamNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(TeamNationalityMaxLength)]
        public string Nationality { get; set; } = null!;

        [Required]
        public int Trophies { get; set; }

        public virtual ICollection<TeamFootballer> TeamsFootballers { get; set; } = new HashSet<TeamFootballer>();
    }
}
