using StudentSupport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupport.BusinessLogic.Services.IServices
{
    public interface IAnswerService
    {
        Task<IEnumerable<Answer>> GetAnswersByQuestionAsync(int questionId);
        Task AddAnswerAsync(Answer answer);
        Task<Answer?> GetByIdAsync(int answerId);
        Task UpdateAnswerAsync(Answer answer);
        Task MarkAsBestAnswerAsync(int answerId);
        Task<bool> UpvoteAnswerAsync(int answerId, int userId);
        Task DeleteAnswerAsync(int id);
    }
}
