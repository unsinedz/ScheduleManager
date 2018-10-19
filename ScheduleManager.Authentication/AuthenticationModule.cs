using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ScheduleManager.Authentication.Identity;
using ScheduleManager.Authentication.Storage;
using ScheduleManager.Domain;

namespace ScheduleManager.Authentication
{
    internal class AuthenticationModule : IApplicationModule
    {
        private readonly AuthenticationModuleOptions _options;

        public AuthenticationModule(AuthenticationModuleOptions options)
        {
            this._options = options;
        }

        public void RegisterDependencies(IServiceCollection services)
        {
            services.ConfigureApplicationCookie(ConfigureAuthentication);
            services.AddDbContext<IdentityContext>(options => options.UseMySql(_options.DatabaseConnectionString,
                builder => builder.MigrationsAssembly("ScheduleManager.Authentication")));
            services.AddIdentity<ApplicationUser, UserRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();
        }

        private void ConfigureAuthentication(CookieAuthenticationOptions options)
        {
            options.LoginPath = _options.LoginPath;
            options.ExpireTimeSpan = _options.Expiration;
            options.SlidingExpiration = _options.UseSlidingExpiration;
            options.LoginPath = _options.LoginPath;
            options.LogoutPath = _options.LogoutPath;
            options.AccessDeniedPath = _options.AccessDeniedPath;

            options.Cookie.Name = _options.Cookie.Name;
            options.Cookie.Path = _options.Cookie.Path;
            options.Cookie.Expiration = _options.Cookie.Expiration;
            options.Cookie.SecurePolicy = _options.Cookie.IsSecure
                ? CookieSecurePolicy.Always
                : CookieSecurePolicy.SameAsRequest;
        }
    }

    public static class AuthenticationModuleRegistrationExtensions
    {
        public static IServiceCollection AddProjectAuthentication(this IServiceCollection services, Action<AuthenticationModuleOptions> optionsBuilder)
        {
            if (optionsBuilder == null)
                throw new ArgumentNullException(nameof(optionsBuilder));

            var options = new AuthenticationModuleOptions();
            optionsBuilder(options);
            new AuthenticationModule(options).RegisterDependencies(services);
            return services;
        }
    }
}