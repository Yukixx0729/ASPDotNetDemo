using System.ComponentModel.DataAnnotations;

namespace Blogorama.Web.Models
{
    public class Register
    {
        public required string Email { get; set; }
        public required string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Passwords are not match.")]
        public required string ConfirmPassword { get; set; }
    }

}