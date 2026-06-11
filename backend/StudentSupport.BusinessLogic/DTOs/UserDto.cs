using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupport.BusinessLogic.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string StudyProgram { get; set; } = string.Empty;
        public int Semester { get; set; }
        public int Points { get; set; }
        public int Level { get; set; }
    }
}
