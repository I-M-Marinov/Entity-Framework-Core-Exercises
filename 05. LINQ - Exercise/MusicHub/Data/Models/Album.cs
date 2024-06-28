using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicHub.Validation;

namespace MusicHub.Data.Models
{
    public class Album
    {


        [Key]
        public int Id { get; set; }
        [MaxLength(ValidationConstants.AlbumNameMaxLength)]
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public DateTime ReleaseDate { get; set; }

        // [NotMapped] // do not map properties that are calculated due to performance issues and/or achieve better normalization
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal Price
        {
            get
            {
                return Songs.Sum(song => song.Price);
            }
        }

        public int? ProducerId { get; set; } // nullable 

        [ForeignKey(nameof(ProducerId))] 
        public Producer Producer { get; set; } = null!;

        public ICollection<Song> Songs { get; set; } = new List<Song>();
    }
}
