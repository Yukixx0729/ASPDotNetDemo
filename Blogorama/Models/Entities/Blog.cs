using System.ComponentModel.DataAnnotations;

namespace Blogorama.Models.Entities
{
    public class Blog
    {
        public Guid BlogId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }

}