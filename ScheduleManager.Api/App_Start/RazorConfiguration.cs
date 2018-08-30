using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;

namespace ScheduleManager.Api
{
    public class RazorConfiguration
    {
        public static void ConfigureRazor(RazorViewEngineOptions options)
        {
            SetViewLocations(options.ViewLocationFormats);
        }

        private static void SetViewLocations(IList<string> locations)
        {
            locations.Clear();
            locations.Add("/Views/Default/{1}/{0}.cshtml");
            locations.Add("/Views/Default/Shared/{0}.cshtml");
            locations.Add("/Views/Default/Layouts/{0}.cshtml");
        }
    }
}