using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artillery.DataProcessor.ExportDto
{
    public class ExportShellsDto
    {
        public double ShellWeight { get; set; }

        public string Caliber { get; set; } = null!;

        public ExportGunsDto[] Guns { get; set; } 
    }
}
