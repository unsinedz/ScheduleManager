using System.Globalization;
using Microsoft.Extensions.Options;
using ScheduleManager.Domain.Extensions;

namespace ScheduleManager.Localizations.Data.Strings
{
    public class StringLocalizationManager : LocalizationManager<string, string>
    {
        public StringLocalizationManager(IOptions<StringLocalizationOptions> options) : base(options.Value?.DefaultCulture)
        {
            options.Value?.Providers?.Each(this.AddResourceProvider);
        }
    }
}