using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DAL
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public int HeadingId { get; set; }
        public Heading Heading { get; set; }

        public List<Subcategory> Subcategories = new List<Subcategory>();
        public List<Announcement> Announcements = new List<Announcement>();
    }
}