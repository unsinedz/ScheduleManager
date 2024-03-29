﻿using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ScheduleManager.Authentication;
using ScheduleManager.Data;
using ScheduleManager.Domain;
using ScheduleManager.Domain.DependencyInjection;
using ScheduleManager.Localizations;
using ScheduleManager.Localizations.Data;
using ScheduleManager.Localizations.Data.Strings;

namespace ScheduleManager.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }

        public string DefaultConnectionString { get; private set; }

        public IHostingEnvironment Environment { get; private set; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            this.Environment = env;
            this.Configuration = configuration;
            this.DefaultConnectionString = Configuration.GetConnectionString("DefaultConnection");
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            TypeResolver.Current = new TypeResolver(app.ApplicationServices);
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            if (Configuration.GetSection("Security")?.GetValue("HttpToHttpsRedirect", false) ?? false)
            {
                app.UseHttpsRedirection();
                app.UseHsts();
            }

            app.UseAuthentication()
                .UseStaticFiles()
                .UseRequestLocalization(options =>
                {
                    options.SupportedCultures = new[]
                    {
                        new CultureInfo("en-US"),
                        new CultureInfo("ru-RU")
                    };
                    options.SupportedUICultures = options.SupportedCultures;
                    options.DefaultRequestCulture = new RequestCulture("en-US");
                })
                .UseMvc(RouteConfig.ConfigureRoutes);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            this.AddProjectModules(services);
            services.AddMvc()
                .ConfigureJson()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddRazorOptions(RazorConfiguration.ConfigureRazor)
                .AddViewLocalization()
                .AddDataAnnotationsLocalization()
                .AddMvcLocalization();

            services.ConfigureMvc()
                .ConfigureIIS()
                .ConfigureIdentity()
                .ConfigureRouteOptions();
        }

        private void AddProjectModules(IServiceCollection services)
        {
            services.Configure<LocalizationModuleOptions>(Configuration.GetSection("Localizations"));
            services.AddProjectApi()
                .AddProjectDomain()
                .AddProjectData(options =>
                    options.DatabaseConnectionString = DefaultConnectionString)
                .AddProjectAuthentication(ConfigureAuthentication)
                .AddProjectLocalization();
        }

        private void ConfigureAuthentication(AuthenticationModuleOptions options)
        {
            var authenticationSettings = Configuration.GetSection("Authentication");
            if (authenticationSettings == null)
                throw new KeyNotFoundException("Authentication section is not present in configuration file.");

            options.DatabaseConnectionString = DefaultConnectionString;
            options.UseSlidingExpiration = authenticationSettings.GetValue("SlidingExpiration", true);
            options.Expiration = TimeSpan.FromMinutes(authenticationSettings.GetValue("TimeoutMinutes", 60));
            options.LoginPath = authenticationSettings.GetValue("LoginPath", "/account/login");
            options.LogoutPath = authenticationSettings.GetValue("LogoutPath", "/account/logout");
            options.AccessDeniedPath = authenticationSettings.GetValue("AccessDeniedPath", "/account/loginrequired");

            var cookieSettings = authenticationSettings.GetSection("Cookie");
            options.Cookie = new AuthenticationCookieOptions
            {
                IsSecure = cookieSettings?.GetValue("IsSecure", false) ?? false,
                Name = cookieSettings?.GetValue("Name", "AuthCookie") ?? "AuthCookie",
                Path = cookieSettings?.GetValue("Path", "/") ?? "/",
                Expiration = TimeSpan.FromMinutes(cookieSettings?.GetValue("ExpirationMinutes", 60) ?? 60)
            };
        }
    }
}
