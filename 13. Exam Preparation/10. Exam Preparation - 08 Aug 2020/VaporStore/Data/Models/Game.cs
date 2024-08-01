
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace VaporStore.Data.Models
{
    public class Game
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public decimal Price { get; set; } // cannot be negative !!!! 

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [ForeignKey(nameof(Developer))]
        public int DeveloperId { get; set; }

        [Required]
        public virtual Developer Developer { get; set; }


        [Required]
        [ForeignKey(nameof(Genre))]
        public int GenreId { get; set; }

        [Required]
        public virtual Genre Genre { get; set; }


        // TODO:
        //•	Purchases - collection of type Purchase
        //•	GameTags - collection of type GameTag.Each game must have at least one tag.

    }
}
