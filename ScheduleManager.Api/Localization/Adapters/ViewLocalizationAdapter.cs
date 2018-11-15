using System.Globalization;
using System.Net;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using ScheduleManager.Localizations.Data.Strings;

namespace ScheduleManager.Api.Localization.Adapters
{
    public class ViewLocalizationAdapter : StringLocalizationManager, IViewLocalizer
    {
        public ViewLocalizationAdapter(IOptions<StringLocalizationOptions> options) : base(options)
        {
        }

        LocalizedHtmlString IHtmlLocalizer.this[string name] =>
            new LocalizedHtmlString(name, WebUtility.HtmlDecode(this.Localize(name, name)));

        LocalizedHtmlString IHtmlLocalizer.this[string name, params object[] arguments] =>
            new LocalizedHtmlString(name, WebUtility.HtmlDecode(string.Format(this.Localize(name, name), arguments)));

        public LocalizedString GetString(string name) => new LocalizedString(name, this.Localize(name, name));

        public LocalizedString GetString(string name, params object[] arguments)
            => new LocalizedString(name, string.Format(this.Localize(name, name), arguments));

        IHtmlLocalizer IHtmlLocalizer.WithCulture(CultureInfo culture) => (IHtmlLocalizer)this.WithCulture(culture);
    }
}