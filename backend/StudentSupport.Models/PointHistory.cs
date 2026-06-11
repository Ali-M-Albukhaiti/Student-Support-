using System;
using System.ComponentModel.DataAnnotations;

namespace StudentSupport.Models
{
    public class PointHistory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int Points { get; set; }

        [Required, MaxLength(200)]
        public string Reason { get; set; } = string.Empty;

        public string? TargetType { get; set; } // "Post" or "Answer"
        public int? TargetId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public User? User { get; set; }
    }
}
