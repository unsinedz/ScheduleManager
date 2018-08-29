using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ScheduleManager.Api.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}