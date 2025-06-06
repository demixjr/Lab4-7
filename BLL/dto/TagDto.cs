namespace BLL.dto
{
    public class TagDto
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        public List<AnnouncementDto> Announcements = new List<AnnouncementDto>();
    }
}
