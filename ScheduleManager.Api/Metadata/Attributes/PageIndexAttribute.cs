using System;
using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Api.ModelBinding;

namespace ScheduleManager.Api.Metadata.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class PageIndexAttribute : ModelBinderAttribute
    {
        public PageIndexAttribute() : base(typeof(PageModelBinder))
        {
        }
    }
}