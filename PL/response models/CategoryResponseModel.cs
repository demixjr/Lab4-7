

using System.Text.Json.Serialization;
using BLL.dto;
using DAL;

namespace PL.response_models
{
    public class CategoryResponseModel
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public List<SubcategoryResponseModel> Subcategories { get; set; }  = new List<SubcategoryResponseModel>();


    }
}
