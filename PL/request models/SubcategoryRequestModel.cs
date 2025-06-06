using System.ComponentModel.DataAnnotations;

namespace PL.request_models
{
    public class SubcategoryRequestModel
    {
        const int minLength = 3;
        const int nameLength = 32;

        [Required(ErrorMessage = "Назва є обов’язковою")]
        [MinLength(minLength, ErrorMessage = "Назва має містити щонайменше 3 символи")]
        [MaxLength(nameLength, ErrorMessage = "Назва не може перевищувати 32 символи")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Назва рубрики є обов’язковою")]
        [MinLength(minLength, ErrorMessage = "Назва категорії має містити щонайменше 3 символи")]
        [MaxLength(nameLength, ErrorMessage = "Назва категорії не може перевищувати 32 символи")]
        public string CategoryName { get; set; }
    }
}
