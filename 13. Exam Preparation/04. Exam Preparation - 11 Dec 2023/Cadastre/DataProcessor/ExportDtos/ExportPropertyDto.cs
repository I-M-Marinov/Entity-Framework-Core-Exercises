using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastre.DataProcessor.ExportDtos
{
    public class ExportPropertyDto
    {
        public string PropertyIdentifier { get; set; } = null!;

        public int  Area { get; set; }

        public string Address { get; set; } = null!;

        public string DateOfAcquisition { get; set; } = null!;

        public ICollection<ExportCitizenDto> Owners { get; set; } = null!;

    }
}
