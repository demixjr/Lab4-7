namespace BLL.dto
{
    public class CategoryDto
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }

        public int HeadingId { get; set; }
        public HeadingDto Heading { get; set; }

        public List<SubcategoryDto> Subcategories = new List<SubcategoryDto>();
        public List<AnnouncementDto> Announcements = new List<AnnouncementDto>();

    }
}
