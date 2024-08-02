
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VaporStore.Data.Models.Enums;

namespace VaporStore.Data.Models
{
    public class Card
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^\d{4} \d{4} \d{4} \d{4}$")]
        public string Number { get; set; } = null!;

        [Required]
        [RegularExpression("^\\d{3}$")]
        [MaxLength(3)]
        public string Cvc { get; set; } = null!;

        [Required]
        public CardType Type { get; set; }

        [Required]
        [ForeignKey(nameof(User))]

        public int UserId { get; set; }

        [Required]
        public virtual User User { get; set; } = null!;

        public virtual ICollection<Purchase> Purchases { get; set; } = new HashSet<Purchase>();



    }
}
