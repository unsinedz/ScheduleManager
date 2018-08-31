using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using ScheduleManager.Domain.Extensions;

namespace ScheduleManager.Localizations.Data.Strings
{
    public class StringLocalizationManager : LocalizationManager<string, string>, IStringLocalizer
    {
        public StringLocalizationManager(IOptions<StringLocalizationOptions> options) : base(options.Value?.DefaultCulture)
        {
            options.Value?.Providers?.Each(this.AddResourceProvider);
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            if (!includeParentCultures)
                return this.GetMatchingProvider(this.DefaultCulture).Values.Select(x => new LocalizedString(x.Key, x.Value));

            return this.Providers.SelectMany(x => x.Value.Values).Select(x => new LocalizedString(x.Key, x.Value));
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            this.DefaultCulture = culture;
            return this;
        }

        public LocalizedString this[string name] => new LocalizedString(name, this.Localize(name, name));

        public LocalizedString this[string name, params object[] arguments] =>
            new LocalizedString(name, string.Format(this.Localize(name, name), arguments));
    }
}