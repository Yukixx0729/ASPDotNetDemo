using Blogorama.Data;
using Blogorama.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using Blogorama.Models.Entities;

namespace Blogorama.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public UsersController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> SignUp(SignupUserViewModel viewModel)
        {
            var hashedPassword = HashPassword(viewModel.Password);

            var user = new User
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Password = hashedPassword,
                RegisteredAt = DateTime.Now
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return Redirect("/");
        }

        //hash password
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);

        }
    }
}