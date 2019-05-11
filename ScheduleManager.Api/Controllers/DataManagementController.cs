using Microsoft.AspNetCore.Mvc;

namespace ScheduleManager.Api.Controllers
{
    public class DataManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}