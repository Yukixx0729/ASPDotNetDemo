

namespace Blogorama.Web.Models
{
    public class Login
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required bool RememberMe { get; set; }

    }

}