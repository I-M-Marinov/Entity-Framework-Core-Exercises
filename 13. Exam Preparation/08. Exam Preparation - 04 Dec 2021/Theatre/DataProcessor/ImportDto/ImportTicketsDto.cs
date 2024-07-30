using System.ComponentModel.DataAnnotations;
using static Theatre.DataConstraints;


namespace Theatre.DataProcessor.ImportDto
{
    public class ImportTicketsDto
    {
        [Required]
        [Range(TicketPriceMinValue, TicketPriceMaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(TicketRowNumberMinValue, TicketRowNumberMaxValue)]
        public sbyte RowNumber { get; set; }

        [Required] 
        public int PlayId { get; set; }
    }
}
