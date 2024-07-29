using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artillery.DataProcessor.ExportDto
{
    public class ExportGunsDto
    {
        public string GunType { get; set; } = null!;

        public int GunWeight { get; set; }

        public double BarrelLength  { get; set; }

        public string Range { get; set; } = null!;
    }
}
