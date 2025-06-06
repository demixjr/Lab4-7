using System.ComponentModel.DataAnnotations;

namespace PL.request_models
{
    public class AnnouncementRequestModel
    {
        const int minLength = 3;
        const int nameLength = 32;
        const int descLength = 256;

        [Required(ErrorMessage = "Назва є обов’язковою")]
        [MinLength(minLength, ErrorMessage = "Назва має містити щонайменше 3 символи")]
        [MaxLength(nameLength, ErrorMessage = "Назва не може перевищувати 32 символи")]
        public string Title { get; set; }


        [Required(ErrorMessage = "Опис є обов’язковим")]
        [MaxLength(descLength, ErrorMessage = "Опис не може перевищувати 256 символів")]
        public string Description { get; set; }
        public CategoryRequestModelWithoutHeading Category { get; set; }
        public SubcategoryRequestModelWithoutCategory Subcategory { get; set; }
        public List<TagRequestModel> Tags { get; set; }

    }
}
