using Blogorama.Web.Models;
using Blogorama.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Blogorama.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;


        public LoginController(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login model, string returnUrl = "/")
        {
            if (ModelState.IsValid)
            {
                var identityResult = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (identityResult.Succeeded)
                {
                    if (returnUrl == null || returnUrl == "/")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToRoute(returnUrl);
                    }
                }
                ModelState.AddModelError("", "Username or password is incorrect.");
            }
            return View();
        }
    }
}