
using System.ComponentModel.DataAnnotations;

namespace VaporStore.Data.Models
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required] 
        [MaxLength(20)] 
        public string Username { get; set; } = null!;

        [Required]
        [RegularExpression(@"^[A-Z][a-z]+ [A-Z][a-z]+$")]
        public string FullName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [Range(3,103)]
        public int Age  { get; set; }

        public virtual ICollection<Card> Cards { get; set; } = new HashSet<Card>();

    }
}
