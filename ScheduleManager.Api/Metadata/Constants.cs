namespace ScheduleManager.Api.Metadata
{
    public static class Constants
    {
        public static class ApiVersions
        {
            public const string V1 = "V1";
        }

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

            public const string Course = "course";

            public const string TimePeriod = "timeperiod";

            public const string Room = "room";

            public const string Subject = "subject";

            public const string Attendee = "attendee";

            public const string Activity = "activity";

            public const string DaySchedule = "dayschedule";
            
            public const string WeekSchedule = "weekschedule";

            public const string ScheduleGroup = "schedulegroup";
        }
    }
}