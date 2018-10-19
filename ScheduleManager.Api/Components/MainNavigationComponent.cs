using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ScheduleManager.Api.Components
{
    public class NavigationViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            await Task.CompletedTask;
            return View("_Navigation");
        }
    }
}