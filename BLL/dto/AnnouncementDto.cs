using DAL;

namespace BLL.dto
{
    public class AnnouncementDto
    {
        public int AnnouncementId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string SubcategoryName { get; set; }

        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; }

        public int SubcategoryId { get; set; }
        public SubcategoryDto Subcategory { get; set; }

        public string Username { get; set; }
        public UserDto User { get; set; }

        public List<TagDto> Tags = new List<TagDto>();

    }
}
