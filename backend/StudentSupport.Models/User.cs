using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StudentSupport.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string StudyProgram { get; set; } = string.Empty;

        [Range(1, 12)]
        public int Semester { get; set; }

        public int Points { get; set; } = 0;
        public int Level { get; set; } = 1;
        public bool IsAdmin { get; set; } = false;

        // Navigation Properties
        [JsonIgnore]
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<PostLike> PostLikes { get; set; } = new List<PostLike>();
        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
        public ICollection<PointHistory> PointHistories { get; set; } = new List<PointHistory>();
        public ICollection<AnswerUpvote> AnswerUpvotes { get; set; } = new List<AnswerUpvote>();
    }
}
