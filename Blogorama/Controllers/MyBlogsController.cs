using Blogorama.Data;
using Blogorama.Models.Entities;
using Blogorama.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blogorama.Web.Controllers
{
    public class MyBlogsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public MyBlogsController(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
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
        [Authorize]
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

            var blogViewModel = new BlogViewModel
            {
                Blog = blog,
                UserName = userName
            };

            return View(blogViewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id)
        {
            var blog = await _dbContext.Blogs.FindAsync(id);

            return View(blog);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(NewBlog model)
        {
            var blog = await _dbContext.Blogs.FindAsync(model.BlogId);
            if (blog is not null)
            {
                blog.Title = model.Title;
                blog.Content = model.Content;
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Details", "MyBlogs", new { id = blog.BlogId });
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var blog = await _dbContext.Blogs.FindAsync(id);

            if (blog != null)
            {
                _dbContext.Blogs.Remove(blog);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("List", "MyBlogs");
            }
            else
            {
                return NotFound();
            }
        }


    }
}