using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trucks.DataProcessor.ExportDto
{
    public class ExportClientsDto
    {
        public string Name { get; set; }

        public ICollection<ExportTrucksDto> Trucks { get; set; } = null!;

    }
}
