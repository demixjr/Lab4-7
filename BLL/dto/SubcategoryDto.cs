namespace BLL.dto
{
    public class SubcategoryDto
    {
        public int SubcategoryId { get; set; }
        public string Name { get; set; }

        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; }
        public List<AnnouncementDto> Announcements = new List<AnnouncementDto>();
    }
}
