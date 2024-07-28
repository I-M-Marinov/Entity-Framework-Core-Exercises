using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footballers.DataProcessor.ExportDto
{
    public class ExportFootballerDto
    {
        public string FootballerName { get; set; }

        public string ContractStartDate { get; set; }

        public string ContractEndDate { get; set; } 

        public string BestSkillType { get; set; }

        public string PositionType { get; set; }
    }
}
