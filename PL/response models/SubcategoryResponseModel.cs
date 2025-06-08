using System.Text.Json.Serialization;

namespace PL.response_models
{
    public class SubcategoryResponseModel
    {
        [JsonIgnore]
        public int SubcategoryId { get; set; }
        public string Name { get; set; }

        public List<AnnouncementResponseModel> Announcements { get; set; } = new List<AnnouncementResponseModel>();
    }
}
