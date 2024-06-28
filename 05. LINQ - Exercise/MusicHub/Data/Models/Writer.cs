using MusicHub.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.Data.Models
{
    public class Writer
    {

        public Writer()
        {
            Songs = new List<Song>();
        }
        [Key]
        public int Id { get; set; }

        [MaxLength(ValidationConstants.WriterNameMaxLength)]
        [Required]
        public string Name { get; set; } = null!;

        public string? Pseudonym { get; set; }

        public ICollection<Song> Songs { get; set; }
    }
}
