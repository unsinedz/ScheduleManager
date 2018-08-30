using System.Collections.Generic;
using System.Globalization;

namespace ScheduleManager.Localizations.Data.Strings
{
    public class StringLocalizationOptions
    {
        public CultureInfo DefaultCulture { get; set; }

        public IEnumerable<ILocalizableResourceProvider<string, string>> Providers { get; set; }
    }
}