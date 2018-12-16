using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.Localization;

namespace ScheduleManager.Api.Metadata
{
    public class ApiDisplayMetadataProvider : IDisplayMetadataProvider
    {
        public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (context.Attributes == null)
                return;

            var validationAttributes = context.Attributes.OfType<ValidationAttribute>();
            foreach (var validationAttribute in validationAttributes)
            {
                if (!string.IsNullOrEmpty(validationAttribute.ErrorMessageResourceName)
                    && validationAttribute.ErrorMessageResourceType == null)
                    validationAttribute.ErrorMessageResourceType = typeof(Object);
            }

            var metadataAwareAttributes = context.Attributes.OfType<IMetadataAware>();
            foreach (var attribute in metadataAwareAttributes)
                attribute.OnDisplayMetadataCreated(context.DisplayMetadata);
        }
    }
}