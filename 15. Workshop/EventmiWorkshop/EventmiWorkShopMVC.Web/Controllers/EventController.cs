using System.Globalization;
using EventmiWorkshop.Data.Models;
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

            bool isStartDateValid = DateTime.TryParse(model.StartDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDateValid);

            bool isEndDateValid = DateTime.TryParse(model.EndDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDateValid);


            if (!isStartDateValid)
            {
                ModelState.AddModelError(nameof(model.StartDate), "Invalid Start Date Format !");
                return View(model);
            }

            if (!isEndDateValid)
            {
                ModelState.AddModelError(nameof(model.EndDate), "Invalid End Date Format !");
                return View(model);
            }

            await this._eventService.AddEvent(model, startDateValid, endDateValid);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                EditEventFormModel eventModel = await this._eventService.GetEventById(id.Value);
                return View(eventModel);
            }
            catch (Exception e)
            {

                return RedirectToAction("Index", "Home");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, EditEventFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            bool isStartDateValid = DateTime.TryParse(model.StartDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDateValid);

            bool isEndDateValid = DateTime.TryParse(model.EndDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDateValid);


            if (!isStartDateValid)
            {
                ModelState.AddModelError(nameof(model.StartDate), "Invalid Start Date Format !");
                return View(model);
            }

            if (!isEndDateValid)
            {
                ModelState.AddModelError(nameof(model.EndDate), "Invalid End Date Format !");
                return View(model);
            }

            try
            {
                await this._eventService.EditEventById(id.Value, model, startDateValid, endDateValid);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e )
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]

        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                EditEventFormModel eventModel = await this._eventService.GetEventById(id.Value);
                return View(eventModel);
            }
            catch (Exception e)
            {

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {

            try
            {
                await this._eventService.DeleteEventById(id);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            try
            {
                List<Event> allEvents = await this._eventService.GetAllEvents();

                List<ViewEventFormModel> eventModels = allEvents.Select(e => new ViewEventFormModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    StartDate = e.StartDate.ToString("MM/dd/yyyy"), 
                    EndDate = e.EndDate.ToString("MM/dd/yyyy"),    
                    Place = e.Place
                }).ToList();

                return View(eventModels);
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> All(List<ViewEventFormModel> models)
        {
            if (!ModelState.IsValid)
            {
                return View(models);
            }

            foreach (var model in models)
            {

                bool isStartDateValid = DateTime.TryParse(model.StartDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDateValid);
                bool isEndDateValid = DateTime.TryParse(model.EndDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDateValid);

                try
                {
                    await this._eventService.AddEvent(new AddEventFormModel
                    {
                        Name = model.Name,
                        Place = model.Place,
                        StartDate = model.StartDate,
                        EndDate = model.EndDate
                    }, startDateValid, endDateValid);

                    return RedirectToAction("All");
                }
                catch (Exception e)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var eventModel = await _eventService.GetEventDetails(id);
                return View(eventModel);
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home");
            }
        }



    }
}
