using System;
using System.ComponentModel.DataAnnotations;

namespace StudentSupport.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(2000)]
        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign Keys
        [Required]
        public int AuthorId { get; set; }

        public int? AnswerId { get; set; }
        public int? PostId { get; set; }
        public int? QuestionId { get; set; }

        // Navigation
        public User? Author { get; set; }
        public Post? Post { get; set; }
        public Answer? Answer { get; set; }
        public Question? Question { get; set; }
    }
}
