using Microsoft.AspNetCore.Mvc;

namespace ScheduleManager.Api.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "Index path";
        }
    }
}