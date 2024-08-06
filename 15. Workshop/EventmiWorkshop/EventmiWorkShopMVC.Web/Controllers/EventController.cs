using EventmiWorkshopMVC.Services.Data.Interfaces;
using EventmiWorkshopMVC.Web.ViewModels.Event;

using Microsoft.AspNetCore.Mvc;

namespace EventmiWorkshopMVC.Web.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            this._eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEventFormModel model)
        {
            if (!ModelState.IsValid) 
            {
                return View(model);
            }

            bool addSuccess = await this._eventService.AddEvent(model);

            if (!addSuccess)
            {
                ModelState.AddModelError(nameof(model.StartDate), "Start or End Date is not in the correct format!");
                ModelState.AddModelError(nameof(model.EndDate), "Start or End Date is not in the correct format!");

                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
