using Blogorama.Data;
using Blogorama.Models.Entities;
using Blogorama.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blogorama.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public BlogsController(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> List()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var userBlogs = _dbContext.Blogs.Where(b => b.UserId == user.Id).ToList();
            return View(userBlogs);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var blog = await _dbContext.Blogs.FirstOrDefaultAsync(b => b.BlogId == id);
            if (blog == null)
            {
                return NotFound();
            }
            var writer = await _userManager.FindByIdAsync(blog.UserId);
            var userName = writer?.UserName ?? "Unknown";
            var comments = await _dbContext.Comments.Where(c => c.LinkedBlogId == id).OrderByDescending(c => c.CreatedAt).ToListAsync();
            var blogViewModel = new BlogViewModel
            {
                Blog = blog,
                UserName = userName,
                Comments = comments
            };

            return View(blogViewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id)
        {
            var blog = await _dbContext.Blogs.FindAsync(id);
            var user = await _userManager.GetUserAsync(User);
            if (blog?.UserId != user?.Id)
            {
                return NotFound();
            }
            return View(blog);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(NewBlog model)
        {
            var blog = await _dbContext.Blogs.FindAsync(model.BlogId);
            var user = await _userManager.GetUserAsync(User);
            if (blog != null && user != null && user.Id == blog.UserId)
            {

                blog.Title = model.Title;
                blog.Content = model.Content;
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Details", "Blogs", new { id = blog.BlogId });
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var blog = await _dbContext.Blogs.FindAsync(id);
            var user = await _userManager.GetUserAsync(User);
            if (blog != null && user != null && user.Id == blog.UserId)
            {
                _dbContext.Blogs.Remove(blog);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("List", "Blogs");
            }
            else
            {
                return NotFound();
            }
        }


    }
}