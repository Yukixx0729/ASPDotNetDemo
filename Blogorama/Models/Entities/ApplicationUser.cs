using Microsoft.AspNetCore.Identity;

namespace Blogorama.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {

        public virtual ICollection<Blog>? Blogs { get; set; }

        public virtual ICollection<Comment>? Comments { get; set; }
    }
}