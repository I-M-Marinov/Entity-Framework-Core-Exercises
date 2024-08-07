using System.ComponentModel.DataAnnotations;
using EventmiWorkshopMVC.Common;
using static EventmiWorkshopMVC.Common.EntityConstraints;

namespace EventmiWorkshop.Data.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(EntityConstraints.Event.EventNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [MaxLength(EntityConstraints.Event.EventPlaceMaxLength)]
        public string Place { get; set; } = null!;

    }
}
