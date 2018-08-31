using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.DependencyInjection;
using ScheduleManager.Api.Localization.Adapters;
using ScheduleManager.Domain;

namespace ScheduleManager.Api
{
    internal class ApiModule : IApplicationModule
    {
        public void RegisterDependencies(IServiceCollection services)
        {
            services.AddSingleton<IViewLocalizer, ViewLocalizationAdapter>();
        }
    }

    public static class ApiModuleRegistrationExtensions
    {
        public static IServiceCollection AddProjectApi(this IServiceCollection services)
        {
            new ApiModule().RegisterDependencies(services);
            return services;
        }
    }
}