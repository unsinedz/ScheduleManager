using Microsoft.Extensions.DependencyInjection;
using ScheduleManager.Domain.Common;
using ScheduleManager.Domain.Faculties;
using ScheduleManager.Domain.Scheduling;

namespace ScheduleManager.Domain
{
    public class DomainModule : IApplicationModule
    {
        public void RegisterDependencies(IServiceCollection services)
        {
            this.RegisterEntities(services);
        }

        protected virtual void RegisterEntities(IServiceCollection services)
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
            services.AddTransient<Schedule>();
            services.AddTransient<WeekSchedule>();
        }
    }
}