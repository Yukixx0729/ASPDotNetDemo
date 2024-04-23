using Blogorama.Models.Entities;

namespace Blogorama.Web.Models
{
    public class BlogViewModel
    {
        public Blog? Blog { get; set; }

        public NewComment NewComment { get; set; }
        public List<Comment> Comments { get; set; }
    }
    public class NewComment
    {
        public required string Content { get; set; }
        public required Guid LinkedBlogId { get; set; }


    }
}