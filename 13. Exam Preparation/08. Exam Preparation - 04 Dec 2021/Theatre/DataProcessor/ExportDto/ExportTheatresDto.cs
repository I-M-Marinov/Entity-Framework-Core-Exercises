﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theatre.DataProcessor.ExportDto
{
    public class ExportTheatresDto
    {

        public string Name { get; set; }

        public int Halls { get; set; }

        public decimal TotalIncome { get; set; }

        public ExportTicketsDto[] Tickets { get; set; }
    }
}
