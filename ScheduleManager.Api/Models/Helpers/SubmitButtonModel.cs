namespace ScheduleManager.Api.Models.Helpers
{
    public class SubmitButtonModel
    {
        public SubmitButtonModel(string text, string materialIcon = null, object htmlAttributes = null)
        {
            this.Text = text;
            this.MaterialIcon = materialIcon;
            this.HtmlAttributes = htmlAttributes;
        }

        public string Text { get; set; }

        public string MaterialIcon { get; set; }

        public object HtmlAttributes { get; set; }
    }
}