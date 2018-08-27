using Microsoft.Extensions.DependencyInjection;
using ScheduleManager.Data.Common;
using ScheduleManager.Data.Entities;
using ScheduleManager.Data.Faculties;
using ScheduleManager.Data.Scheduling;
using ScheduleManager.Domain;
using ScheduleManager.Domain.Common;
using ScheduleManager.Domain.Entities;
using ScheduleManager.Domain.Faculties;
using ScheduleManager.Domain.Scheduling;

namespace ScheduleManager.Data
{
    public class DataModule : IApplicationModule
    {
        public void RegisterDependencies(IServiceCollection services)
        {
            services.AddEntityFrameworkMySql();
            this.RegisterContexts(services);
            this.RegisterServices(services);
        }

        protected virtual void RegisterContexts(IServiceCollection services)
        {
            services.AddDbContext<CommonContext>();
            services.AddDbContext<FacultyContext>();
            services.AddDbContext<ScheduleContext>();
        }

        protected virtual void RegisterServices(IServiceCollection services)
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
}