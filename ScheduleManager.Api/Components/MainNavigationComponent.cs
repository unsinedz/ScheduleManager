using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ScheduleManager.Api.Components
{
    public class NavigationViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            await Task.CompletedTask;
            if (HttpContext.User.Identity.IsAuthenticated)
                return View("_Navigation");

            return Content(string.Empty);
        }
    }
}