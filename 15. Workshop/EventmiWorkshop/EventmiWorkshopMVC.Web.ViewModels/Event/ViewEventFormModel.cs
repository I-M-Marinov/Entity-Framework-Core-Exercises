using EventmiWorkshopMVC.Common;
using System.ComponentModel.DataAnnotations;


namespace EventmiWorkshopMVC.Web.ViewModels.Event
{
    public class ViewEventFormModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(EntityConstraints.Event.EventNameMinLength)]
        [MaxLength(EntityConstraints.Event.EventNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]

        public string StartDate { get; set; } = null!;

        [Required]
        public string EndDate { get; set; } = null!;

        [Required]

        [MinLength(EntityConstraints.Event.EventPlaceMinLength)]
        [MaxLength(EntityConstraints.Event.EventPlaceMaxLength)]
        public string Place { get; set; } = null!;

        [Required]
        public bool IsActive { get; set; }
    }
}
