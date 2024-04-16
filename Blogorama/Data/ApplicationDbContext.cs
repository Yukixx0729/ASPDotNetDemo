
using Microsoft.EntityFrameworkCore;
using Blogorama.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Blogorama.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Comment> Comments { get; set; }
    }


}