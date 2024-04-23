using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogorama.Models.Entities
{
    public class Comment
    {
        public Guid CommentId { get; set; }

        [Required]
        [MaxLength(1000)]
        public required string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        [ForeignKey("UserId")]
        public required string UserId { get; set; }

        public virtual required ApplicationUser ApplicationUser { get; set; }

        [Required]
        [ForeignKey("BlogId")]
        public required Guid LinkedBlogId { get; set; }

        public virtual required Blog Blog { get; set; }
    }

}