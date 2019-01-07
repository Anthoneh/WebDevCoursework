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

namespace WebDevCoursework.Controllers
{
    [Authorize]
    public class ForumsController : Controller
    {
        private readonly ApplicationDbContext _context;
        //add the user here and in the constructor

        public ForumsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Forums
        public async Task<IActionResult> Index()
        {
            return View(await _context.Forum.ToListAsync());
        }

        // GET: Forums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Forum forum = await _context.Forum
                .FirstOrDefaultAsync(m => m.Id == id);
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
            //Protection from overposting
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

        // GET: Forums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Forums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Forum forum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(forum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(forum);
        }

        // GET: Forums/Edit/5
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

        // POST: Forums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Forums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Forum forum = await _context.Forum
                .FirstOrDefaultAsync(m => m.Id == id);
            if (forum == null)
            {
                return NotFound();
            }

            return View(forum);
        }

        // POST: Forums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Forum forum = await _context.Forum.FindAsync(id);
            _context.Forum.Remove(forum);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ForumExists(int id)
        {
            return _context.Forum.Any(e => e.Id == id);
        }
    }
}
