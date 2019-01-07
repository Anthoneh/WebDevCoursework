using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebDevCoursework.Models
{
    public class User : IdentityUser
    {
        [MaxLength(56)]
        public string CustomMessage { get; set; }
        
        //public IdentityRole UserRole { get; set; }
    }
}
