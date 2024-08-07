
using EventmiWorkshop.Data;
using EventmiWorkshop.Data.Models;
using EventmiWorkshopMVC.Services.Data.Interfaces;
using EventmiWorkshopMVC.Web.ViewModels.Event;
using Microsoft.EntityFrameworkCore;

namespace EventmiWorkshopMVC.Services.Data
{
    public class EventService : IEventService
    {

        private readonly EventmiDbContext dbContext;

        public EventService(EventmiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddEvent(AddEventFormModel eventFormModel, DateTime startDate, DateTime endDate)
        {
            
            Event newEvent = new Event()
            {
                Name = eventFormModel.Name,
                StartDate = startDate,
                EndDate = endDate,
                Place = eventFormModel.Place
            };

           await dbContext.Events.AddAsync(newEvent);
           await dbContext.SaveChangesAsync();

        }

        public async Task<EditEventFormModel> GetEventById(int id)
        {
            Event? eventDb = await this.dbContext.Events
                .FirstOrDefaultAsync(e => e.Id == id);

            if (eventDb == null)
            {
                throw new ArgumentNullException();
            }

            if (!eventDb.IsActive)
            {
                throw new InvalidOperationException();
            }


            EditEventFormModel eventFound =  new EditEventFormModel()
            {
                Name = eventDb.Name,
                StartDate = eventDb.StartDate.ToString("G"),
                EndDate = eventDb.EndDate.ToString("G"),
                Place = eventDb.Place
            };

            return eventFound;
        }

        public async Task EditEventById(int id, EditEventFormModel eventFormModel, DateTime startDate, DateTime endDate)
        {
            Event eventToEdit = await dbContext.Events
                .FirstAsync(e => e.Id == id);

            if (!eventToEdit.IsActive)
            {
                throw new InvalidOperationException();
            }

            eventToEdit.Name = eventFormModel.Name;
            eventToEdit.StartDate = startDate;
            eventToEdit.EndDate = endDate;
            eventToEdit.Place = eventFormModel.Place;

            await dbContext.SaveChangesAsync();
        }
    }
}
