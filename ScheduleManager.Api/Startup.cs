using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ScheduleManager.Authentication;
using ScheduleManager.Data;
using ScheduleManager.Domain;
using ScheduleManager.Domain.DependencyInjection;

namespace ScheduleManager.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
            this.DefaultConnectionString = Configuration.GetConnectionString("DefaultConnection");
        }

        public IConfiguration Configuration { get; }

        public string DefaultConnectionString { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddRazorOptions(RazorConfiguration.ConfigureRazor)
                .AddControllersAsServices();
            
            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = true;
            });
        }

        private void InitializeModules(IServiceCollection services)
        {
            new DomainModule().RegisterDependencies(services);
            new DataModule(new DataModuleOptions
            {
                DatabaseConnectionString = DefaultConnectionString
            })
            .RegisterDependencies(services);

            #region Authentication module

            var authenticationSettings = Configuration.GetSection("Authentication");
            if (authenticationSettings == null)
                throw new KeyNotFoundException("Authentication section is not present in configuration file.");

            var cookieSettings = authenticationSettings.GetSection("Cookie");
            var authenticationModuleOptions = new AuthenticationModuleOptions
            {
                DatabaseConnectionString = DefaultConnectionString,
                UseSlidingExpiration = authenticationSettings.GetValue("SlidingExpiration", true),
                Expiration = TimeSpan.FromMinutes(authenticationSettings.GetValue("TimeoutMinutes", 60)),
                LoginPath = authenticationSettings.GetValue("LoginPath", "/account/login"),
                LogoutPath = authenticationSettings.GetValue("LogoutPath", "/account/logout"),
                AccessDeniedPath = authenticationSettings.GetValue("AccessDeniedPath", "/account/loginrequired"),
                Cookie = new AuthenticationCookieOptions
                {
                    IsSecure = cookieSettings?.GetValue("IsSecure", false) ?? false,
                    Name = cookieSettings?.GetValue("Name", "AuthCookie") ?? "AuthCookie",
                    Path = cookieSettings?.GetValue("Path", "/") ?? "/",
                    Expiration = TimeSpan.FromMinutes(cookieSettings?.GetValue("ExpirationMinutes", 60) ?? 60)
                }
            };
            new AuthenticationModule(authenticationModuleOptions).RegisterDependencies(services);

            #endregion
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            TypeResolver.Current = new TypeResolver(app.ApplicationServices);
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
