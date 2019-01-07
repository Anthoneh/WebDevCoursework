using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDevCoursework.Models
{
    //A post will need the Usersname of the person posting, the forum it is posted in, (the text field?,)the date and the time.
    public class Post
    {
        public DateTime TimePosted { get; set; }
        public int Id { get; set; }
        public string Comment { get; set; }

        public virtual Forum ForumId { get; set; }
        //public virtual User UserId { get; set; }
    }
}
