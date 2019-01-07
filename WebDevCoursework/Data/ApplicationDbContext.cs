using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebDevCoursework.Models;

namespace WebDevCoursework.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<WebDevCoursework.Models.Forum> Forum { get; set; }
        public DbSet<WebDevCoursework.Models.Post> Post { get; set; }
        public DbSet<WebDevCoursework.Models.User> User { get; set; }
    }
}
