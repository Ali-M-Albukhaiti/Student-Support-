using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentSupport.Models
{
    public class PostLike
    {
        [Required]
        public int PostId { get; set; }

        [Required]
        public int UserId { get; set; }

        // Navigation
        public Post? Post { get; set; }
        public User? User { get; set; }
    }
}
