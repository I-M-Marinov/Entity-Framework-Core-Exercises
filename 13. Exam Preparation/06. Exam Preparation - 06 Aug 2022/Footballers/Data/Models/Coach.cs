using System.ComponentModel.DataAnnotations;
using static Footballers.DataConstraints;


namespace Footballers.Data.Models
{
    public class Coach
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(CoachNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        public string Nationality { get; set; } = null!;

        public virtual ICollection<Footballer> Footballers { get; set; } = new List<Footballer>();
    }
}
