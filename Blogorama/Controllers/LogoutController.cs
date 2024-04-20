using Blogorama.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blogorama.Web.Controllers
{
    public class LogoutController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;


        public LogoutController(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ComfirmLogout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Login");

        }

        [HttpPost]
        public IActionResult DontLogout()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}