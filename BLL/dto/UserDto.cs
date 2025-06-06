using System.Collections.Generic;
using DAL;

namespace BLL.dto
{
    public class UserDto
    {
        public string Username { get; set; }
        public string Password { get;set;}
        public List<AnnouncementDto> Announcements = new List<AnnouncementDto>();

    }
}
