using System;

namespace StudentSupport.BusinessLogic.DTOs
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Semester { get; set; }
        public int AuthorId { get; set; }
        public UserDto? Author { get; set; } = null!;
    }
}