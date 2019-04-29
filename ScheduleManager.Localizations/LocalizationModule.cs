using System.Globalization;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ScheduleManager.Domain;
using ScheduleManager.Localizations.Data;
using ScheduleManager.Localizations.Data.Strings;

namespace ScheduleManager.Localizations
{
    internal class LocalizationModule : IApplicationModule
    {
        public void RegisterDependencies(IServiceCollection services)
        {
            services.AddSingleton<IStringLocalizer, StringLocalizationManager>();
            using (var provider = services.BuildServiceProvider())
            {
                var logger = provider.GetService<ILogger<JsonStringProvider>>();
                var moduleOptions = provider.GetRequiredService<IOptions<LocalizationModuleOptions>>();
                if ((moduleOptions.Value.Resources?.Length).GetValueOrDefault() == 0)
                {
                    logger.LogError($"[Localizations]: No resources found in configuration.");
                }

                services.Configure<StringLocalizationOptions>(options =>
                {
                    options.DefaultCulture = new CultureInfo("en-US");
                    options.Providers = moduleOptions.Value.Resources.Select(x => new JsonStringProvider(x, logger)).ToArray();
                });
            }
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
