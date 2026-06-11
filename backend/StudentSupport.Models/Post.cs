using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentSupport.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        [Range(1, 8)]
        public int Semester { get; set; }

        // Foreign Key
        public int AuthorId { get; set; }

        // Navigation
        public User? Author { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<PostLike> PostLikes { get; set; } = new List<PostLike>();
    }
}
