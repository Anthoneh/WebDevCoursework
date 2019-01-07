using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDevCoursework.Models;
using WebDevCoursework.Data;
using Microsoft.EntityFrameworkCore;

namespace WebDevCoursework.Repository
{
    public class ForumRepository : IForumRepository
    {
        private ApplicationDbContext _context;

        public ForumRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Forum forum)
        {
            _context.Forum.AddAsync(forum).Wait();
            _context.SaveChangesAsync().Wait();
        }

        public void Delete(Forum forum)
        {
            _context.Forum.Remove(forum);
            _context.SaveChanges();
        }

        public void Edit(Forum forum)
        {
            _context.Forum.Update(forum);
            _context.SaveChanges();
        }

        public async Task<Forum> GetForumIdAsync(int? id)
        {
            Forum forum = await _context.Forum
                .FirstOrDefaultAsync(m => m.Id == id);
            return forum;
        }

        public List<Forum> ListOfForums()
        {
            return _context.Forum.ToList();
        }
    }
}
