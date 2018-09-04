using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ScheduleManager.Api.Views
{
    public static class HtmlExtensions
    {
        public static IHtmlContent AttrEditorFor<TModel, TResult>(this IHtmlHelper<TModel> html,
            Expression<Func<TModel, TResult>> expression, object htmlAttributes)
        {
            return html.EditorFor(expression, new { htmlAttributes = htmlAttributes });
        }
    }
}