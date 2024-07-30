using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using Theatre.Data.Models.Enums;
using static Theatre.DataConstraints;

namespace Theatre.Data.Models
{
    public class Play
    {
        [Key]
        public int Id { get; set; }

        [Required] 
        [MaxLength(PlayTitleMaxLength)] 
        public string Title { get; set; } = null!;
        [Required]
        [Range(typeof(TimeSpan), "01:00:00", "23:59:59")]
        public TimeSpan Duration { get; set; }

        [Required]
        [Range(PlayRatingMinLength,PlayRatingMaxLength)]

        public float Rating { get; set; }

        [Required]
        public Genre Genre { get; set; }

        [Required]
        [StringLength(PlayDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(PlayScreenwriterMaxLength)]

        public string Screenwriter { get; set; } = null!;

        public virtual ICollection<Cast> Casts { get; set; } = new HashSet<Cast>();

        public virtual ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
    }
}
