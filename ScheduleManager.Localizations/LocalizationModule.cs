using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using ScheduleManager.Domain;
using ScheduleManager.Localizations.Data.Strings;

namespace ScheduleManager.Localizations
{
    internal class LocalizationModule : IApplicationModule
    {
        public void RegisterDependencies(IServiceCollection services)
        {
            services.AddSingleton<IStringLocalizer, StringLocalizationManager>();
        }
    }

    public static class LocalizationModuleRegistrationExtensions
    {
        public static IServiceCollection AddProjectLocalization(this IServiceCollection services)
        {
            new LocalizationModule().RegisterDependencies(services);
            return services;
        }
    }
}
