using System;
using System.Linq;
using System.Collections.Generic;
using ScheduleManager.Domain.Extensions;

namespace ScheduleManager.Localizations.Data
{
    public class LocalizationDescriptor<TLocalization, TKey, TValue> : IDisposable
        where TLocalization : Localization<TKey, TValue>
    {
        public string CultureCode { get; set; }

        public IEnumerable<TLocalization> Localizations { get; set; }

        public void Dispose()
        {
            this.Localizations.Each(x => { x.Dispose(); });
            this.Localizations = null;
            CultureCode = null;
            GC.SuppressFinalize(this);
        }
    }
}