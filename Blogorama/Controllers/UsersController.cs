using Blogorama.Data;
using Blogorama.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using Blogorama.Models.Entities;
using Microsoft.EntityFrameworkCore;

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
            bool userExists = await CheckUserExists(viewModel.Email);
            if (userExists)
            {
                TempData["Error"] = "Invalid email.";
                return RedirectToAction("SignUp");
            }

            byte[] salt = GenerateSalt();
            byte[] hashedPassword = HashPassword(viewModel.Password, salt);

            string saltString = Convert.ToBase64String(salt);
            string hashedPasswordString = Convert.ToBase64String(hashedPassword);

            var user = new User
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Password = saltString + ":" + hashedPasswordString,
                RegisteredAt = DateTime.Now
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return Redirect("/");
        }
        //generate salt
        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        //hash password
        private byte[] HashPassword(string password, byte[] salt)
        {
            const int iterations = 10000;
            const int derivedKeyLength = 32;

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(derivedKeyLength);
            }
        }


        private async Task<bool> CheckUserExists(string email)
        {
            var user = await dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
            return user != null;

        }
    }
}