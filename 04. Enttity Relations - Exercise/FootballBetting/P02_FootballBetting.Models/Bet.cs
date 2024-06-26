using P02_FootballBetting.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P02_FootballBetting.Data.Models.Enumerations;

namespace P02_FootballBetting.Data.Models
{
    public  class Bet
    {
        [Key]
        public int BetId { get; set; }
        public decimal Amount { get; set; }

        [MaxLength(ValidationConstants.PredictionMaxLength)]
        [Required]
        public PredictionResult Prediction { get; set; }
        public DateTime DateTime { get; set; }
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; } 

        public int GameId { get; set; }
        [ForeignKey(nameof(GameId))] 
        public Game? Game { get; set; } 
    }
}
