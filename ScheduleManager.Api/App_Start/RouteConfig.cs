using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace ScheduleManager.Api
{
    public static class RouteConfig
    {
        public static void ConfigureRoutes(IRouteBuilder builder)
        {
            builder.AddControllerRoute("Home", "{action}", "Index");
            builder.AddControllerRoute("Account", "account/{action}", "Login");
            builder.AddControllerRoute("Faculty", "faculties/{action}", "List");
            builder.AddControllerRoute("Department", "departments/{action}", "List");
            ConfigureApiV1Routes(builder);
        }

        private static void ConfigureApiV1Routes(IRouteBuilder builder)
        {
            var apiVersion = "V1";
            builder.AddApiRoutes("Department", $"api/{apiVersion}/departments", apiVersion, hasList: true, hasSingle: true);
            builder.AddApiRoutes("Faculty", $"api/{apiVersion}/faculties", apiVersion, hasList: true, hasSingle: true);
            builder.AddApiRoutes("Lecturer", $"api/{apiVersion}/lecturers", apiVersion, hasList: true, hasSingle: true);
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

        public static void AddApiRoutes(this IRouteBuilder builder, string controllerName, string url, string version, bool hasList = true, bool hasSingle = true)
        {
            if (string.IsNullOrWhiteSpace(controllerName))
                throw new System.ArgumentException("Controller name is not specified.", nameof(controllerName));

            if (string.IsNullOrWhiteSpace(url))
                throw new System.ArgumentException("Url is not specified.", nameof(url));

            var areaPrefix = $"Api";
            var areaName = $"{areaPrefix}_{version}";
            if (hasList)
            {
                builder.MapAreaRoute($"{areaPrefix}_{controllerName}_List", areaName, url, new
                {
                    controller = controllerName,
                    action = "List"
                });
            }

            if (hasSingle)
            {
                builder.MapAreaRoute($"{areaPrefix}_{controllerName}", areaName, $"{url.TrimEnd('/')}/{{id?}}", new
                {
                    controller = controllerName,
                    action = "Item"
                });
            }
        }
    }
}