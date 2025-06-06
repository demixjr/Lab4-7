using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Heading
    {
        public int HeadingId { get; set; }
        public string Name { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
