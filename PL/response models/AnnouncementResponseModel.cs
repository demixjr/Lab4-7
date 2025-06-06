
using System.Text.Json.Serialization;

namespace PL.response_models
{
    public class AnnouncementResponseModel
    {
        public int AnnouncementId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        [JsonIgnore]
        public CategoryResponseModel Category { get; set; }


        [JsonIgnore]
        public int SubcategoryId { get; set; }
        public string SubcategoryName { get; set; }

        [JsonIgnore]
        public SubcategoryResponseModel Subcategory { get; set; }

        public string Username { get; set; }
        [JsonIgnore]
        public UserResponseModel User { get; set; }

        public List<TagResponseModel> Tags = new List<TagResponseModel>();
    }
}
