using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Blogorama.Models;
using Microsoft.AspNetCore.Identity;
using Blogorama.Models.Entities;
using Blogorama.Data;
using Blogorama.Web.Models;
using Microsoft.EntityFrameworkCore;

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
        var blogs = _dbContext.Blogs
            .Include(b => b.ApplicationUser)
            .OrderByDescending(b => b.CreatedAt)
            .ToList();
        var blogViewModels = blogs.Select(blog => new BlogViewModel
        {
            Blog = blog,
        }).ToList();
        return View(blogViewModels);
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
