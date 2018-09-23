using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.DependencyInjection;
using ScheduleManager.Api.Localization.Adapters;
using ScheduleManager.Api.Metadata;
using ScheduleManager.Domain;

namespace ScheduleManager.Api
{
    internal class ApiModule : IApplicationModule
    {
        public static readonly ApiModule Current = new ApiModule();

        public void RegisterDependencies(IServiceCollection services)
        {
            services.AddSingleton<IViewLocalizer, ViewLocalizationAdapter>();
        }

        public void ConfigureMvc(MvcOptions options)
        {
            options.ModelMetadataDetailsProviders.Add(new ApiDisplayMetadataProvider());
        }
    }

    public static class ApiModuleRegistrationExtensions
    {
        public static IServiceCollection AddProjectApi(this IServiceCollection services)
        {
            ApiModule.Current.RegisterDependencies(services);
            return services;
        }

        public static IServiceCollection ConfigureMvc(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(ApiModule.Current.ConfigureMvc);
            return services;
        }
    }
}