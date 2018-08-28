using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ScheduleManager.Domain;

namespace ScheduleManager.Authentication
{
    public class AuthenticationModule : IApplicationModule
    {
        private readonly AuthenticationModuleOptions _options;

        public AuthenticationModule(AuthenticationModuleOptions options)
        {
            this._options = options;
        }

        public void RegisterDependencies(IServiceCollection services)
        {
            services.AddAuthentication().AddCookie(ConfigureAuthentication);
        }

        protected virtual void ConfigureAuthentication(CookieAuthenticationOptions options)
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
}