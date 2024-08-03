using System.ComponentModel.DataAnnotations;
using static TravelAgency.DataConstraints;

namespace TravelAgency.DataProcessor.ImportDtos
{
    public class ImportBookingsDto
    {

        [Required]
        public string BookingDate { get; set; } = null!;

        [Required]
        [MinLength(CustomerFullNameMinLength)]
        [MaxLength(CustomerFullNameMaxLength)]
        public string CustomerName { get; set; } = null!;

        [Required]
        [MinLength(TourPackageNameMinLength)]
        [MaxLength(TourPackageNameMaxLength)]
        public string TourPackageName { get; set; } = null!;
    }
}
