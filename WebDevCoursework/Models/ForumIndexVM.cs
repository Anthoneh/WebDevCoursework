using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDevCoursework.Models
{
    public class ForumIndexVM
    {
        public Forum Forum { get; set; }
        public List<Forum> Forums { get; set; }

        public int ForumId { get; set; }
        public string ForumTitle { get; set; }
        public string Description { get; set; }
    }
}
