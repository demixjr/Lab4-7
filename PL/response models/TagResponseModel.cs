using System.Text.Json.Serialization;

namespace PL.response_models
{
    public class TagResponseModel
    {
        [JsonIgnore]
        public int TagId { get; set; }
        public string Name { get; set; }
        public List<AnnouncementResponseModel> Announcements { get; set; } = new List<AnnouncementResponseModel>();
    }
}
