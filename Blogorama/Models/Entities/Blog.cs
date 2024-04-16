using System.ComponentModel.DataAnnotations;

namespace Blogorama.Models.Entities
{
    public class Blog
    {
        public Guid BlogId { get; set; }


        [MaxLength(50)]
        public required string Title { get; set; }


        public required string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public required string UserId { get; set; }

        public virtual required ApplicationUser ApplicationUser { get; set; }
    }

}