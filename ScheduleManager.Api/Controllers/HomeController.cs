using Microsoft.AspNetCore.Mvc;

namespace ScheduleManager.Api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}