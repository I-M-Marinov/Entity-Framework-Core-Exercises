using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query.Internal;
using MusicHub.Validation;

namespace MusicHub.Data.Models
{
    public class Performer
    {

        [Key]
        public int Id { get; set; }

        [MaxLength(ValidationConstants.PerformerFirstNameMaxLength)]
        [Required]
        public string FirstName { get; set; } = null!;

        [MaxLength(ValidationConstants.PerformerLastNameMaxLength)]
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public int Age { get; set; }
        [Required]
        public decimal NetWorth { get; set; }

        public ICollection<SongPerformer> PerformerSongs { get; set; } = new List<SongPerformer>();
    }
}
