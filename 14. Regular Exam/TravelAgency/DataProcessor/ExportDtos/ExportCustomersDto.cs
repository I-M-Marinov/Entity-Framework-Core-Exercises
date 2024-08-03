using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Data.Models;

namespace TravelAgency.DataProcessor.ExportDtos
{
    public class ExportCustomersDto
    {
        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public ExportBookingsDto[] Bookings { get; set; }
    }
}
