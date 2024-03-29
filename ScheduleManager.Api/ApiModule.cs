using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using ScheduleManager.Api.Localization;
using ScheduleManager.Api.Localization.Adapters;
using ScheduleManager.Api.Metadata;
using ScheduleManager.Api.Metadata.Attributes.Validation;
using ScheduleManager.Api.Serialization.Json;
using ScheduleManager.Domain;

namespace ScheduleManager.Api
{
    internal class ApiModule : IApplicationModule
    {
        public static readonly ApiModule Current = new ApiModule();

        public void RegisterDependencies(IServiceCollection services)
        {
            services.AddSingleton<IViewLocalizer, ViewLocalizationAdapter>();
            services.AddSingleton<IStringLocalizerFactory, StringLocalizerFactory>();
            services.AddSingleton<IValidationAttributeAdapterProvider, DefaultValidationAttributeAdapterProvider>();
        }

        public void ConfigureMvc(MvcOptions options)
        {
            options.ModelMetadataDetailsProviders.Add(new ApiDisplayMetadataProvider());
        }

        public void ConfigureIIS(IISOptions options)
        {
            options.ForwardClientCertificate = true;
        }

        public void ConfigureIdentity(IdentityOptions options)
        {
            options.Password.RequireDigit = false;
            options.Password.RequiredUniqueChars = 0;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;

            options.SignIn.RequireConfirmedEmail = true;
            options.SignIn.RequireConfirmedPhoneNumber = false;

            options.Lockout.AllowedForNewUsers = true;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
        }

        public void ConfigureRouteOptions(RouteOptions options)
        {
            options.LowercaseUrls = true;
        }

        public void ConfigureJson(MvcJsonOptions options)
        {
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            options.SerializerSettings.ContractResolver = new IgnoreProxiesLowercaseContractResolver();
            JsonConvert.DefaultSettings = () => options.SerializerSettings;
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

        public static IServiceCollection ConfigureIIS(this IServiceCollection services)
        {
            services.Configure<IISOptions>(ApiModule.Current.ConfigureIIS);
            return services;
        }

        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(ApiModule.Current.ConfigureIdentity);
            return services;
        }

        public static IServiceCollection ConfigureRouteOptions(this IServiceCollection services)
        {
            services.Configure<RouteOptions>(ApiModule.Current.ConfigureRouteOptions);
            return services;
        }

        public static IMvcBuilder ConfigureJson(this IMvcBuilder builder)
        {
            builder.AddJsonOptions(ApiModule.Current.ConfigureJson);
            return builder;
        }
    }
}