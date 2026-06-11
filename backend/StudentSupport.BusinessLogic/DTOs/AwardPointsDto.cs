using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupport.BusinessLogic.DTOs
{
    public class AwardPointsDto
    {
        public int UserId { get; set; }
        public int Points { get; set; }
        public string Reason { get; set; } = string.Empty;

        // Optional: for linking points to posts/answers
        public string? TargetType { get; set; } // "Post" or "Answer"
        public int? TargetId { get; set; }
    }
}
