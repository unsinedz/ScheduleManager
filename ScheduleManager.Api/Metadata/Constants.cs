namespace ScheduleManager.Api.Metadata
{
    public static class Constants
    {
        public static class Keys
        {
            public const string CreatePageTitle = "CreatePageTitle";

            public const string EditPageTitle = "EditPageTitle";

            public const string RelatedEntityFetchRoute = "RelatedEntityFetchRoute";

            public const string RelatedEntityFetchRouteArea = "RelatedEntityFetchRouteArea";

            public const string RelatedEntityType = "RelatedEntityName";
            
            public const string RelatedEntitySelectMultiple = "RelatedEntitySelectMultiple";

            public const string RelatedEntityRequired = "RelatedEntityRequired";

            public const string RelatedEntityErrorMessage = "RelatedEntityErrorMessage";
        }

        public static class EntityType {
            public const string Faculty = "faculty";
            
            public const string Department = "department";

            public const string Lecturer = "lecturer";
        }
    }
}