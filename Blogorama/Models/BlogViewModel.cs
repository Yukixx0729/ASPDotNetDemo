

using Blogorama.Models.Entities;

namespace Blogorama.Web.Models
{
    public class BlogViewModel
    {
        public Blog? Blog { get; set; }
        public required string UserName { get; set; }
    }
}