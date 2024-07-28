using Boardgames.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Boardgames.Data.DataConstraints;

namespace Boardgames.Data.Models
{
    public class Boardgame
    {

        [Key]
        [Required]
        public int Id { get; set; }


        [Required]
        [MaxLength(BoardGameNameMaxLength)]
        public string Name { get; set; } = null!;


        [Required]
        public double Rating { get; set; }

        [Required]
        [MaxLength(BoardGameYearPublishedMaxValue)]
        public int YearPublished { get; set; }
         
        [Required]
        public CategoryType CategoryType { get; set; }

        [Required] 
        public string Mechanics  { get; set; } = null!;
         
        [Required]
        [ForeignKey(nameof(Creator))]
        public int CreatorId { get; set; }

        [Required]
        public virtual Creator Creator { get; set; } = null!;

        public virtual ICollection<BoardgameSeller> BoardgamesSellers { get; set; } = new HashSet<BoardgameSeller>();
    }
}
