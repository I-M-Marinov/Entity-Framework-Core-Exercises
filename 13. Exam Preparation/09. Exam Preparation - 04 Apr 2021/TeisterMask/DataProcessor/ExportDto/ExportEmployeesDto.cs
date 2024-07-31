using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeisterMask.DataProcessor.ExportDto
{
    public class ExportEmployeesDto
    {
        public string Username { get; set; }

        public ExportTaskDto[] Tasks { get; set; }

    }
}
