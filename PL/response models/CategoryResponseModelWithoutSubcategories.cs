using System.Text.Json.Serialization;

namespace PL.response_models
{
    public class CategoryResponseModelWithoutSubcategories
    {
        public string Name { get; set; }
        [JsonIgnore]
        public int CategoryId { get; set; }
    }
}
