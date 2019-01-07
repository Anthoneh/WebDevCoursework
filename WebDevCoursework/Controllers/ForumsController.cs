using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebDevCoursework.Data;
using WebDevCoursework.Models;
using WebDevCoursework.Repository;

namespace WebDevCoursework.Controllers
{
    [Authorize]
    public class ForumsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ForumRepository _forumRep;

        public ForumsController(ApplicationDbContext context)
        {
            _context = context;
            _forumRep = new ForumRepository(_context);
        }


        public IActionResult Index()
        {
            ForumIndexVM viewModel = new ForumIndexVM();

            viewModel.Forums = _forumRep.ListOfForums();

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Forum forum = await _forumRep.GetForumIdAsync(id);
            if (forum == null)
            {
                return NotFound();
            }

            ForumDetailsVM viewModel = await GetDetailsFromVM(forum);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details([Bind("ForumId,Comment,TimePosted")] ForumDetailsVM viewModel)
        {
            if (ModelState.IsValid)
            {
                Post post = new Post();

                post.Comment = viewModel.Comment;
                post.TimePosted = viewModel.TimePosted;

                Forum forum = await _context.Forum
                .FirstOrDefaultAsync(m => m.Id == viewModel.ForumId);
                if (forum == null)
                {
                    return NotFound();
                }

                post.ForumId = forum;
                _context.Post.Add(post);
                await _context.SaveChangesAsync();

                viewModel = await GetDetailsFromVM(forum);
            }

            return View(viewModel);
        }

        private async Task<ForumDetailsVM> GetDetailsFromVM(Forum forum)
        {
            ForumDetailsVM viewModel = new ForumDetailsVM();

            viewModel.Forum = forum;

            List<Post> posts = await _context.Post
                .Where(m => m.ForumId == forum).ToListAsync();

            viewModel.Posts = posts;

            return viewModel;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            Forum forum = new Forum();
            return View(forum);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name")] Forum forum)
        {
            if (ModelState.IsValid)
            {
                _forumRep.Create(forum);
                return RedirectToAction(nameof(Index));
            }
            return View(forum);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var forum = await _context.Forum.FindAsync(id);
            if (forum == null)
            {
                return NotFound();
            }
            return View(forum);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Forum forum)
        {
            if (id != forum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(forum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ForumExists(forum.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(forum);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Forum forum = await _forumRep.GetForumIdAsync(id);
            if (forum == null)
            {
                return NotFound();
            }

            return View(forum);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Forum forum = await _forumRep.GetForumIdAsync(id);
            foreach (Post post in _context.Post)
            {
                if (post.ForumId == forum)
                {
                    _context.Post.Remove(post);
                }
            }
            _forumRep.Delete(forum);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Search(string searchString)
        {
            var forums = from m in _context.Forum
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                forums = forums.Where(s => s.Name.Contains(searchString));
            }

            return View(await forums.ToListAsync());
        }

        private bool ForumExists(int id)
        {
            return _context.Forum.Any(e => e.Id == id);
        }
    }
}
