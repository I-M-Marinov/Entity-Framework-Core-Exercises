using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventmiWorkshopMVC.Web.ViewModels.Event;

namespace EventmiWorkshopMVC.Services.Data.Interfaces
{
    public interface IEventService 

    { 
        Task<bool> AddEvent (AddEventFormModel eventFormModel);
    }
}
