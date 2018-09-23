using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

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

            var metadataAwareAttributes = context.Attributes.OfType<IMetadataAware>();
            foreach (var attribute in metadataAwareAttributes)
                attribute.OnDisplayMetadataCreated(context.DisplayMetadata);
        }
    }
}