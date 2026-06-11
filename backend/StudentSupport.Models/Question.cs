using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentSupport.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required, MaxLength(10000)]
        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Foreign Key
        [Required]
        public int AuthorId { get; set; }

        // Navigation
        public User? Author { get; set; }
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}
