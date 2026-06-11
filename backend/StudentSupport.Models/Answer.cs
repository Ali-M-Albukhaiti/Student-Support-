using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentSupport.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(5000)]
        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsBest { get; set; } = false;
        public int Upvotes { get; set; } = 0;

        // Foreign Keys
        [Required]
        public int AuthorId { get; set; }

        [Required]
        public int QuestionId { get; set; }

        // Navigation
        public User? Author { get; set; }
        public Question? Question { get; set; }

        public ICollection<AnswerUpvote> UpvotesList { get; set; } = new List<AnswerUpvote>();

    }
}
