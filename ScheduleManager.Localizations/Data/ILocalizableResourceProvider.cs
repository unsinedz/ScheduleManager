using System;
using System.Collections.Generic;
using System.Globalization;

namespace ScheduleManager.Localizations.Data
{
    public interface ILocalizableResourceProvider<TKey, TResource> : IDisposable
    {
        IDictionary<TKey, TResource> Values { get; }

        TResource Get(TKey key);

        CultureInfo GetCulture();
    }
}