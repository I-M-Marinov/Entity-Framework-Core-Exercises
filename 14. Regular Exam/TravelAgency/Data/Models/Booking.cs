﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TravelAgency.Data.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        [Required]
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }

        [Required]
        public virtual Customer Customer { get; set; }

        [Required]
        [ForeignKey(nameof(TourPackage))]
        public int TourPackageId { get; set; }

        [Required]
        public virtual TourPackage TourPackage { get; set; }
    }
}
