using System;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ScheduleManager.Api.Models.Helpers;

namespace ScheduleManager.Api.Views
{
    public static class HtmlExtensions
    {
        public static IHtmlContent AttrEditorFor<TModel, TResult>(this IHtmlHelper<TModel> html,
            Expression<Func<TModel, TResult>> expression, object htmlAttributes)
        {
            return html.EditorFor(expression, new { htmlAttributes = htmlAttributes });
        }

        public static Task<IHtmlContent> LinkButtonAsync<TModel>(this IHtmlHelper<TModel> html, string url,
            string text, string materialIcon = null, object htmlAttributes = null)
        {
            return html.PartialAsync("LinkButton", new LinkButtonModel(url, text, materialIcon, htmlAttributes));
        }

        public static Task<IHtmlContent> SubmitButtonAsync<TModel>(this IHtmlHelper<TModel> html, string text,
            string materialIcon = null, object htmlAttributes = null)
        {
            return html.PartialAsync("SubmitButton", new SubmitButtonModel(text, materialIcon, htmlAttributes));
        }

        public static IHtmlContent RenderAttributes<TModel>(this IHtmlHelper<TModel> html, object htmlAttributes)
        {
            if (htmlAttributes == null)
                return null;

            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            var builder = new StringBuilder();
            foreach (var attribute in attributes)
                builder.Append($"{attribute.Key}=\"{html.Encode(attribute.Value)}\" ");

            return html.Raw(builder.ToString());
        }
    }
}