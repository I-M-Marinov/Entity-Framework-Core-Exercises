using System.ComponentModel.Design;
using System.Globalization;
using EventmiWorkshop.Data;
using EventmiWorkshop.Data.Models;
using EventmiWorkshopMVC.Services.Data.Interfaces;
using EventmiWorkshopMVC.Web.ViewModels.Event;

namespace EventmiWorkshopMVC.Services.Data
{
    public class EventService : IEventService
    {

        private readonly EventmiDbContext dbContext;

        public EventService(EventmiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<bool> AddEvent(AddEventFormModel eventFormModel)
        {
            bool isStartDateValid = DateTime.TryParse(eventFormModel.StartDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate);

            bool isEndDateValid = DateTime.TryParse(eventFormModel.EndDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ednDate);

            if (!isStartDateValid || !isEndDateValid)
            {
                return false;
            }

            Event newEvent = new Event()
            {
                Name = eventFormModel.Name,
                StartDate = startDate,
                EndDate = ednDate,
                Place = eventFormModel.Place
            };

           await dbContext.Events.AddAsync(newEvent);
           await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
