using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupport.Models
{
    public class AnswerUpvote
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AnswerId { get; set; }

        [Required]
        public int UserId { get; set; }

        public Answer? Answer { get; set; }
        public User? User { get; set; }
    }

}
