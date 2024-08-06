using EventmiWorkshopMVC.Web.ViewModels.Event;

using Microsoft.AspNetCore.Mvc;

namespace EventmiWorkshopMVC.Web.Controllers
{
    public class EventController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddEventFormModel model)
        {
            if (!ModelState.IsValid) 
            {
                return View(model);
            }

            return View();
        }
    }
}
