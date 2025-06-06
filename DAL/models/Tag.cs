using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DAL
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; }

        public List<Announcement> Announcements = new List<Announcement>();
    }
}
