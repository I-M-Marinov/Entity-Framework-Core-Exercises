using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.Data.Models
{
    public class SongPerformer
    {

        public int SongId { get; set; }
        [ForeignKey(nameof(SongId))]
        [Required]
        public Song Song { get; set; } = null!;


        public int PerformerId { get; set; }

        [ForeignKey(nameof(PerformerId))]
        [Required]
        public Performer Performer { get; set; } = null!;

    }
}
