using Microsoft.Extensions.DependencyInjection;
using ScheduleManager.Domain.Common;
using ScheduleManager.Domain.Faculties;
using ScheduleManager.Domain.Scheduling;

namespace ScheduleManager.Domain
{
    internal class DomainModule : IApplicationModule
    {
        public void RegisterDependencies(IServiceCollection services)
        {
            this.RegisterEntities(services);
        }

        private void RegisterEntities(IServiceCollection services)
        {
            services.AddTransient<Attendee>();
            services.AddTransient<Course>();
            services.AddTransient<Room>();
            services.AddTransient<Subject>();
            services.AddTransient<TimePeriod>();
            services.AddTransient<Department>();
            services.AddTransient<Faculty>();
            services.AddTransient<Lecturer>();
            services.AddTransient<Activity>();
            services.AddTransient<DaySchedule>();
            services.AddTransient<WeekSchedule>();
            services.AddTransient<ScheduleGroup>();
        }
    }

    public static class DomainModuleRegistrationExtensions
    {
        public static IServiceCollection AddProjectDomain(this IServiceCollection services)
        {
            new DomainModule().RegisterDependencies(services);
            return services;
        }
    }
}