using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footballers.Data.Models
{
    public class TeamFootballer
    {
        [ForeignKey(nameof(Team))]
        [Required]
        public int TeamId { get; set; }

        [Required]
        public virtual Team Team { get; set; } = null!;

        [ForeignKey(nameof(Footballer))]
        [Required]
        public int FootballerId { get; set; }

        [Required]
        public virtual Footballer Footballer { get; set; } = null!;
    }
}
