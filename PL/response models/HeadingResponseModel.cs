

using System.Text.Json.Serialization;

namespace PL.response_models
{
    public class HeadingResponseModel
    {
        public int HeadingId { get; set; }
        public string Name { get; set; }
        public List<CategoryResponseModelWithoutSubcategories> Categories { get; set; } = new List<CategoryResponseModelWithoutSubcategories>();

    }
}
