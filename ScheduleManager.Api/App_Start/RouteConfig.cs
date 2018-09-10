using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace ScheduleManager.Api
{
    public static class RouteConfig
    {
        public static void ConfigureRoutes(IRouteBuilder builder)
        {
            builder.AddControllerRoute("Account", "account/{action}", "Login");
            builder.AddControllerRoute("Home", "{action}", "Index");
            builder.AddControllerRoute("Faculty", "faculties/{action}", "List");
            builder.AddControllerRoute("Department", "departments/{action}", "List");
        }

        public static void AddControllerRoute(this IRouteBuilder builder, string controllerName, string url, string defaultActionName)
        {
            if (string.IsNullOrEmpty(controllerName))
                throw new System.ArgumentException("Controller name can not be empty.", nameof(controllerName));

            if (string.IsNullOrEmpty(url))
                throw new System.ArgumentException("Url can not be empty.", nameof(url));

            if (string.IsNullOrEmpty(defaultActionName))
                throw new System.ArgumentException("Default action name can not be empty.", nameof(defaultActionName));

            builder.MapRoute(controllerName, url, new
            {
                controller = controllerName,
                action = defaultActionName
            });
        }

        public static void AddLocalizableControllerRoute(this IRouteBuilder builder, string controllerName, string url, string defaultActionName)
        {
            if (string.IsNullOrEmpty(controllerName))
                throw new System.ArgumentException("Controller name can not be empty.", nameof(controllerName));

            if (string.IsNullOrEmpty(url))
                throw new System.ArgumentException("Url can not be empty.", nameof(url));

            if (string.IsNullOrEmpty(defaultActionName))
                throw new System.ArgumentException("Default action name can not be empty.", nameof(defaultActionName));

            builder.MapRoute(controllerName, "{culture}/" + url.TrimStart('/'), new
            {
                controller = controllerName,
                action = defaultActionName,
                culture = "en-US"
            });
        }
    }
}