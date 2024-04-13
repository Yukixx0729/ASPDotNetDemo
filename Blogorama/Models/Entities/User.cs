using System.ComponentModel.DataAnnotations;

namespace Blogorama.Models.Entities
{
    public class User
    {


        public Guid UserId { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public DateTime RegisteredAt { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}