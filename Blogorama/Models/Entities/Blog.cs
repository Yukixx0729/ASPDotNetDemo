using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogorama.Models.Entities
{
    public class Blog
    {
        public Guid BlogId { get; set; }

        [MaxLength(50)]
        public required string Title { get; set; }


        public required string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        [ForeignKey("UserId")]
        public required string UserId { get; set; }

        public virtual required ApplicationUser ApplicationUser { get; set; }
    }

}