using StudentSupport.Models;
using System.Threading.Tasks;

namespace StudentSupport.DataAccess.Repositories.IRepositories
{
    public interface IAnswerUpvoteRepo
    {
        Task<AnswerUpvote?> GetByUserAndAnswerAsync(int userId, int answerId);
        Task<AnswerUpvote> AddAsync(AnswerUpvote upvote);
        Task RemoveAsync(AnswerUpvote upvote);
        Task<int> CountByAnswerIdAsync(int answerId);
    }
}
