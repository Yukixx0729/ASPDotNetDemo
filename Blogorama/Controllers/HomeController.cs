using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Blogorama.Models;
using Microsoft.AspNetCore.Identity;
using Blogorama.Models.Entities;
using Blogorama.Data;
using Blogorama.Web.Models;

namespace Blogorama.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _dbContext;

    public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _userManager = userManager;
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var blogs = _dbContext.Blogs.OrderByDescending(b => b.CreatedAt).ToList();
        var blogViewModels = blogs.Select(blog =>
            {
                var user = _userManager.FindByIdAsync(blog.UserId).Result;
                var userName = user?.UserName ?? "Unknown";
                return new BlogViewModel
                {
                    Blog = blog,
                    UserName = userName
                };
            }).ToList();
        return View(blogViewModels);
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
