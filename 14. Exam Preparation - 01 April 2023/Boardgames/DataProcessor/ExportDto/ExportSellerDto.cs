using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boardgames.Data.Models;

namespace Boardgames.DataProcessor.ExportDto
{
    public class ExportSellerDto
    {

        [Required] 
        public string Name { get; set; } = null!;

        [Required] 
        public string Website { get; set; } = null!;

        public ICollection<ExportBoardgameDto> Boardgames { get; set; } = null!;

    }
}
