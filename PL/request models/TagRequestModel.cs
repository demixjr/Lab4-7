using System.ComponentModel.DataAnnotations;

namespace PL.request_models
{
    public class TagRequestModel
    {
        [Required(ErrorMessage = "Назва є обов’язковою")]
        public string Name { get; set; }
    }
}
