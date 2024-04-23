using Blogorama.Data;
using Blogorama.Models.Entities;
using Blogorama.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blogorama.Web.Controllers
{
    public class CommentsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public CommentsController(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create(Guid blogId)
        {
            ViewBag.BlogId = blogId;
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(NewComment model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var blog = await _dbContext.Blogs.FindAsync(model.LinkedBlogId);
                if (user is not null && blog is not null)
                {
                    var comment = new Comment
                    {
                        Content = model.Content,
                        LinkedBlogId = model.LinkedBlogId,
                        UserId = user.Id,
                        CreatedAt = DateTime.Now,
                        ApplicationUser = user,
                        Blog = blog
                    };
                    _dbContext.Comments.Add(comment);
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction("Details", "Blogs", new { id = model.LinkedBlogId });
                }
            }
            return RedirectToAction("Index", "Home");
        }


    }
}