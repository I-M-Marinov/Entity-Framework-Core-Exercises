using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using static Theatre.DataConstraints;

namespace Theatre.Data.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(TicketPriceMinValue, TicketPriceMaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(TicketRowNumberMinValue,TicketRowNumberMaxValue)]
        public sbyte RowNumber { get; set; }

        [Required]
        [ForeignKey(nameof(Play))]

        public int PlayId { get; set; }

        [Required] 
        public virtual Play Play { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Theatre))]

        public int TheatreId { get; set; }

        [Required]
        public virtual Theatre Theatre { get; set; } = null!;

    }
}
