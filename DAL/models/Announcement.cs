using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DAL
{
    public class Announcement
    {
        public int AnnouncementId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }


   
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string CategoryName { get; set; }
        public int SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }
        public string SubcategoryName { get; set; }

        public string Username { get; set; }
        public User User { get; set; }

        public List<Tag> Tags = new List<Tag>();
    }
}
