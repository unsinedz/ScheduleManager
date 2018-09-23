using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace ScheduleManager.Api.Metadata
{
    public interface IMetadataAware
    {
         void OnDisplayMetadataCreated(DisplayMetadata metadata);
    }
}