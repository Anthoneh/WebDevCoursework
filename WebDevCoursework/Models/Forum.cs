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

        [MaxLength(240)]
        public string Name { get; set; }

        [MaxLength(56)]
        public string Description { get; set; }

    }
}
