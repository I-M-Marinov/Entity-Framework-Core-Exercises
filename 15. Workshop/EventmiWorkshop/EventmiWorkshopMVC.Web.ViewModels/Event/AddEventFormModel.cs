using System.ComponentModel.DataAnnotations;
using EventmiWorkshopMVC.Common;

namespace EventmiWorkshopMVC.Web.ViewModels.Event
{
    using static Common.EntityConstraints;

    public class AddEventFormModel
    {
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

    }
}
