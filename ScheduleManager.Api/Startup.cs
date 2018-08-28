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
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                DatabaseConnectionString = Configuration.GetConnectionString("DefaultConnection")
            })
            .RegisterDependencies(services);

            var authenticationSettings = Configuration.GetSection("Authentication");
            if (authenticationSettings == null)
                throw new KeyNotFoundException("Authentication section is not present in configuration file.");

            new AuthenticationModule(new AuthenticationModuleOptions
            {
                UseSlidingExpiration = authenticationSettings.GetValue("SlidingExpiration", true),
                Expiration = TimeSpan.FromMinutes(authenticationSettings.GetValue("TimeoutMinutes", 60)),
                LoginPath = authenticationSettings.GetValue("LoginPath", "/account/login"),
                LogoutPath = authenticationSettings.GetValue("LogoutPath", "/account/logout"),
                AccessDeniedPath = authenticationSettings.GetValue("AccessDeniedPath", "/account/loginrequired")
            }).RegisterDependencies(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            TypeResolver.Current = new TypeResolver(app.ApplicationServices);
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
