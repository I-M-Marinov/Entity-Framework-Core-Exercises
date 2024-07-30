using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Theatre.DataProcessor.ExportDto
{
    public class ExportTicketsDto
    {
        public decimal Price { get; set; }

        public int RowNumber { get; set; }
    }
}
