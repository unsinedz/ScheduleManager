using System;

namespace ScheduleManager.Domain.DependencyInjection
{
    public class TypeResolver : IServiceProvider
    {
        #region Static members

        private static TypeResolver _current;

        public static TypeResolver Current
        {
            get => _current;
            set => SetResolver(value);
        }

        private static void SetResolver(TypeResolver resolver)
        {
            if (_current != null)
                throw new InvalidOperationException("Type resolver was already initialized.");

            _current = resolver;
        }

        public static T Resolve<T>()
        {
            var resolver = TypeResolver.Current;
            if (resolver == null)
                throw new InvalidOperationException("Type resolver was not initialized.");

            return resolver.GetService<T>();
        }

        #endregion

        private readonly IServiceProvider _serviceProvider;

        public TypeResolver(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public T GetService<T>()
        {
            return (T)this.GetService(typeof(T));
        }

        public object GetService(Type serviceType)
        {
            if (_serviceProvider == null)
                throw new InvalidOperationException("Service provider was not initialized.");

            return _serviceProvider.GetService(serviceType);
        }
    }
}