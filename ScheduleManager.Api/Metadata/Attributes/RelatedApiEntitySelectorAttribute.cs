namespace ScheduleManager.Api.Metadata.Attributes
{
    public class RelatedApiEntitySelectorAttribute : RelatedEntitySelectorAttribute
    {
        protected string routeArea = "Api";
        public override string RouteArea
        {
            get => routeArea + (string.IsNullOrEmpty(ApiVersion)
                ? string.Empty
                : $"_{ApiVersion}");
                set => routeArea = value;
        }

        public virtual string ApiVersion { get; set; }

        public RelatedApiEntitySelectorAttribute(string routeName) : base(routeName)
        {
        }
    }
}