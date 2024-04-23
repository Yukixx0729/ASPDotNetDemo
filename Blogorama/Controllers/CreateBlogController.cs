using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Blogorama.Web.Models;
using Blogorama.Models.Entities;
using Blogorama.Data;
using Microsoft.AspNetCore.Authorization;


namespace Blogorama.Web.Controllers
{
    public class CreateBlogController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public CreateBlogController(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreateBlog()
        {
            return View();

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateBlog(NewBlog model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user is not null)
                {
                    var blog = new Blog
                    {
                        Title = model.Title,
                        Content = model.Content,
                        CreatedAt = DateTime.Now,
                        ApplicationUser = user,
                        UserId = user.Id
                    };
                    _dbContext.Blogs.Add(blog);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }

            }
            return RedirectToAction("List", "Blogs");
        }
    }
}