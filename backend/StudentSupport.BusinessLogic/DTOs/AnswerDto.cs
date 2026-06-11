using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupport.BusinessLogic.DTOs
{
    public class AnswerDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsBest { get; set; }
        public int Upvotes { get; set; }
        public int AuthorId { get; set; }
        public int QuestionId { get; set; }
        public UserDto? Author { get; set; }
        // UpvotesList excluded - we only need the count
    }
}
