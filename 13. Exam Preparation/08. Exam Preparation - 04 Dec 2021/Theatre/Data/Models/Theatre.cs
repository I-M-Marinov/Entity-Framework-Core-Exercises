
using System.ComponentModel.DataAnnotations;
using static Theatre.DataConstraints;

namespace Theatre.Data.Models
{
    public class Theatre
    {
        [Key]
        public int Id { get; set; }

        [Required] 
        [MaxLength(TheatreNameMaxLength)] 
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(TheatreNumberOfHallsMaxValue)]

        public sbyte NumberOfHalls { get; set; }


        [Required]
        [MaxLength(TheatreDirectorMaxLength)]
        public string Director { get; set; } = null!;

        public virtual ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();

    }
}
