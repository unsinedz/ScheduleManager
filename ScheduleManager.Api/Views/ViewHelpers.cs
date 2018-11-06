using System;
using Microsoft.AspNetCore.Html;

namespace ScheduleManager.Api.Views
{
    public static class ViewHelpers
    {
        public static IHtmlContent When(bool condition, Func<IHtmlContent> render)
        {
            if (render == null)
                throw new ArgumentNullException(nameof(render));

            if (condition)
                return render();

            return null;
        }

        public static IHtmlContent Attribute(string name, string value = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Attribute name is not specified.", nameof(name));

            return string.IsNullOrEmpty(value)
                ? new HtmlString($"{name}")
                : new HtmlString($"{name}=\"{value}\"");
        }

        public static IHtmlContent ToHtml(string str) => new HtmlString(str);
    }
}