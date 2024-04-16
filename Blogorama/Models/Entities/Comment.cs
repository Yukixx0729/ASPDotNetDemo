using System.ComponentModel.DataAnnotations;

namespace Blogorama.Models.Entities
{
    public class Comment
    {
        public Guid CommentId { get; set; }

        [Required]
        [MaxLength(300)]
        public required string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public required string UserId { get; set; }

        public virtual required ApplicationUser ApplicationUser { get; set; }
    }

}