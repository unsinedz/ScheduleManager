namespace ScheduleManager.Api.Models.Helpers
{
    public class LinkButtonModel
    {
        public LinkButtonModel(string url, string text, string materialIcon = null, object htmlAttributes = null)
        {
            this.Url = url;
            this.Text = text;
            this.MaterialIcon = materialIcon;
            this.HtmlAttributes = htmlAttributes;
        }

        public string Url { get; set; }

        public string Text { get; set; }

        public string MaterialIcon { get; set; }

        public object HtmlAttributes { get; set; }
    }
}