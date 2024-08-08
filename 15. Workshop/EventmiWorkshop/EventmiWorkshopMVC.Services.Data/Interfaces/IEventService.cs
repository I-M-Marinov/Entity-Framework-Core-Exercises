using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventmiWorkshop.Data.Models;
using EventmiWorkshopMVC.Web.ViewModels.Event;

namespace EventmiWorkshopMVC.Services.Data.Interfaces
{
    public interface IEventService 

    { 
        Task AddEvent (AddEventFormModel eventFormModel, DateTime startDate, DateTime endDate);

        Task<EditEventFormModel> GetEventById(int id);

        Task EditEventById(int id, EditEventFormModel eventFormModel, DateTime startDate, DateTime endDate);
            
        Task DeleteEventById(int id);

        Task<List<Event>> GetAllEvents();
        Task<ViewEventFormModel> GetEventDetails(int id);

    }
}
