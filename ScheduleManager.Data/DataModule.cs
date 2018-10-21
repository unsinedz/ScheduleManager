using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using ScheduleManager.Data.Common;
using ScheduleManager.Data.Faculties;
using ScheduleManager.Data.Scheduling;
using ScheduleManager.Domain;
using ScheduleManager.Domain.Common;
using ScheduleManager.Domain.Entities;
using ScheduleManager.Domain.Faculties;
using ScheduleManager.Domain.Scheduling;

namespace ScheduleManager.Data
{
    internal class DataModule : IApplicationModule
    {
        private readonly DataModuleOptions _options;

        public DataModule(DataModuleOptions options)
        {
            this._options = options;
        }

        public void RegisterDependencies(IServiceCollection services)
        {
            services.AddEntityFrameworkMySql();
            this.RegisterContexts(services);
            this.RegisterServices(services);
        }

        private void RegisterContexts(IServiceCollection services)
        {
            services.AddDbContext<CommonContext>(options => options.UseLazyLoadingProxies().UseMySql(_options.DatabaseConnectionString, ConfigureMySqlContext));
            services.AddDbContext<FacultyContext>(options => options.UseLazyLoadingProxies().UseMySql(_options.DatabaseConnectionString, ConfigureMySqlContext));
            services.AddDbContext<ScheduleContext>(options => options.UseLazyLoadingProxies().UseMySql(_options.DatabaseConnectionString, ConfigureMySqlContext));
        }

        private void ConfigureMySqlContext(MySqlDbContextOptionsBuilder builder) => builder.MigrationsAssembly("ScheduleManager.Data");

        private void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IAsyncProvider<Attendee>, CommonProvider<Attendee>>();
            services.AddScoped<IAsyncProvider<Course>, CommonProvider<Course>>();
            services.AddScoped<IAsyncProvider<Room>, CommonProvider<Room>>();
            services.AddScoped<IAsyncProvider<Subject>, CommonProvider<Subject>>();
            services.AddScoped<IAsyncProvider<TimePeriod>, CommonProvider<TimePeriod>>();

            services.AddScoped<IAsyncProvider<Department>, FacultyProvider<Department>>();
            services.AddScoped<IAsyncProvider<Faculty>, FacultyProvider<Faculty>>();
            services.AddScoped<IAsyncProvider<Lecturer>, FacultyProvider<Lecturer>>();

            services.AddScoped<IAsyncProvider<Activity>, ScheduleProvider<Activity>>();
            services.AddScoped<IAsyncProvider<ScheduleGroup>, ScheduleProvider<ScheduleGroup>>();
            services.AddScoped<IAsyncProvider<DaySchedule>, ScheduleProvider<DaySchedule>>();
            services.AddScoped<IAsyncProvider<WeekSchedule>, ScheduleProvider<WeekSchedule>>();
        }
    }

    public static class DataModuleRegistrationExtensions
    {
        public static IServiceCollection AddProjectData(this IServiceCollection services, Action<DataModuleOptions> optionsBuilder)
        {
            if (optionsBuilder == null)
                throw new ArgumentNullException(nameof(optionsBuilder));

            var options = new DataModuleOptions();
            optionsBuilder(options);
            new DataModule(options).RegisterDependencies(services);
            return services;
        }
    }
}