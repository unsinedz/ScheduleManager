using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using ScheduleManager.Api.Metadata;

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
            builder.AddControllerRoute("Lecturer", "lecturers/{action}", "List");
            builder.AddControllerRoute("Attendee", "attendees/{action}", "List");
            builder.AddControllerRoute("Course", "courses/{action}", "List");
            builder.AddControllerRoute("Room", "rooms/{action}", "List");
            builder.AddControllerRoute("Subject", "subjects/{action}", "List");
            builder.AddControllerRoute("TimePeriod", "timeperiods/{action}", "List");
            builder.AddControllerRoute("Activity", "activities/{action}", "List");
            ConfigureApiV1Routes(builder);
        }

        private static void ConfigureApiV1Routes(IRouteBuilder builder)
        {
            var apiVersion = Constants.ApiVersions.V1;
            builder.AddApiRoutes("Department", $"api/{apiVersion}/departments", apiVersion, hasList: true, hasSingle: true);
            builder.AddApiRoutes("Faculty", $"api/{apiVersion}/faculties", apiVersion, hasList: true, hasSingle: true);
            builder.AddApiRoutes("Lecturer", $"api/{apiVersion}/lecturers", apiVersion, hasList: true, hasSingle: true);
            builder.AddApiRoutes("Attendee", $"api/{apiVersion}/attendees", apiVersion, hasList: true, hasSingle: true);
            builder.AddApiRoutes("Course", $"api/{apiVersion}/courses", apiVersion, hasList: true, hasSingle: true);
            builder.AddApiRoutes("Room", $"api/{apiVersion}/rooms", apiVersion, hasList: true, hasSingle: true);
            builder.AddApiRoutes("Subject", $"api/{apiVersion}/subjects", apiVersion, hasList: true, hasSingle: true);
            builder.AddApiRoutes("TimePeriod", $"api/{apiVersion}/timeperiods", apiVersion, hasList: true, hasSingle: true);
            builder.AddApiRoutes("Activity", $"api/{apiVersion}/activities", apiVersion, hasList: true, hasSingle: true);
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