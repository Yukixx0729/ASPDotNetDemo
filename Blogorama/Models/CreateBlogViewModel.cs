namespace Blogorama.Web.Models
{
    public class NewBlog
    {
        public Guid BlogId { get; set; }
        public required string Title { get; set; }

        public required string Content { get; set; }

    }
}