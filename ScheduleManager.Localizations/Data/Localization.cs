using System;

namespace ScheduleManager.Localizations.Data
{
    public class Localization<TKey, TValue> : IDisposable
    {
        public TKey Key { get; set; }

        public TValue Value { get; set; }

        public void Dispose()
        {
            var disposed = false;
            if (Key is IDisposable disposableKey && (disposed = true))
                disposableKey.Dispose();

            if (Value is IDisposable disposableValue && (disposed = true))
                disposableValue.Dispose();

            if (disposed)
                GC.SuppressFinalize(this);
        }
    }
}