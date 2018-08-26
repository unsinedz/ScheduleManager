using Microsoft.Extensions.DependencyInjection;

namespace ScheduleManager.Domain
{
    public interface IApplicationModule
    {
        void RegisterDependencies(IServiceCollection services);
    }
}