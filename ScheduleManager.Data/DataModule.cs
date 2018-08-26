using Microsoft.Extensions.DependencyInjection;
using ScheduleManager.Domain;

namespace ScheduleManager.Data
{
    public class DataModule : IApplicationModule
    {
        public void RegisterDependencies(IServiceCollection services)
        {
            this.RegisterManagers(services);
        }

        protected virtual void RegisterManagers(IServiceCollection services)
        {
        }
    }
}