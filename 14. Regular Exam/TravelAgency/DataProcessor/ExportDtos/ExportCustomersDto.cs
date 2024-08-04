

namespace TravelAgency.DataProcessor.ExportDtos
{
    public class ExportCustomersDto
    {
        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public ExportBookingsDto[] Bookings { get; set; }
    }
}
