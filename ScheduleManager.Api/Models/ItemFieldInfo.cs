
namespace ScheduleManager.Api.Models
{
    public class ItemFieldInfo
    {
        public ItemFieldInfo(string name = null, string value = null)
        {
            this.FieldName = name;
            this.FieldValue = value;
        }

        public string FieldName { get; set; }   

        public string FieldValue { get; set; }
    }
}