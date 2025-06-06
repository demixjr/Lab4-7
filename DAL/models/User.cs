using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DAL
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Announcement> Announcements = new List<Announcement>();
    }
}
