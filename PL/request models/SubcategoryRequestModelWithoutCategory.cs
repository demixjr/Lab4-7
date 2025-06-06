using System.ComponentModel.DataAnnotations;

namespace PL.request_models
{
    public class SubcategoryRequestModelWithoutCategory
    {
        const int minLength = 3;
        const int nameLength = 32;

        [Required(ErrorMessage = "Назва є обов’язковою")]
        [MinLength(minLength, ErrorMessage = "Назва має містити щонайменше 3 символи")]
        [MaxLength(nameLength, ErrorMessage = "Назва не може перевищувати 32 символи")]
        public string Name { get; set; }
    }
}
