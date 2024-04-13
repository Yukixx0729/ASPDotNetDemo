using System.ComponentModel.DataAnnotations;

namespace Blogorama.Models.Entities
{
    public class Comment
    {
        public Guid CommentId { get; set; }

        [Required]
        [MaxLength(300)]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }

}