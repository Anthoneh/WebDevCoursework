using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDevCoursework.Models
{
    public class ForumDetailsVM
    {
        public Forum Forum { get; set; }
        public List<Post> Posts { get; set; }

        public int ForumId { get; set; }
        public string Comment { get; set; }
        public DateTime TimePosted { get; set; }
    }
}
