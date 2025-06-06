namespace BLL.dto
{
    public class HeadingDto
    {
        public int HeadingId { get; set; }
        public string Name { get; set; }
        public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
    }
}
