using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebDevCoursework.Models
{
    public class Forum
    {
        [Key]
        public int Id { get; set; }
        //add limits to Forum names
        public string Name { get; set; }
    }
}
