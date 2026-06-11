using StudentSupport.DataAccess.Repositories.IRepositories;
using StudentSupport.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentSupport.Tests.FakeRepo
{
    public class FakeAnswerUpvoteRepo : IAnswerUpvoteRepo
    {
        public List<AnswerUpvote> AnswerUpvotes { get; set; } = new List<AnswerUpvote>();

        public Task<AnswerUpvote> AddAsync(AnswerUpvote answerUpvote)
        {
            answerUpvote.Id = AnswerUpvotes.Count + 1;
            AnswerUpvotes.Add(answerUpvote);
            return Task.FromResult(answerUpvote);   
        }

        public Task<AnswerUpvote?> GetByAnswerIdAndUserIdAsync(int answerId, int userId)
        {
            var upvote = AnswerUpvotes.FirstOrDefault(a => a.AnswerId == answerId && a.UserId == userId);
            return Task.FromResult(upvote);
        }

        public Task<int> CountByAnswerIdAsync(int answerId)
        {
            var count = AnswerUpvotes.Count(a => a.AnswerId == answerId);
            return Task.FromResult(count);
        }

        public Task RemoveAsync(AnswerUpvote upvote)
        {
            AnswerUpvotes.Remove(upvote);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<AnswerUpvote>> GetByAnswerIdAsync(int answerId)
        {
            var list = AnswerUpvotes.Where(a => a.AnswerId == answerId).AsEnumerable();
            return Task.FromResult(list);
        }

        public Task<AnswerUpvote?> GetByUserAndAnswerAsync(int userId, int answerId)
        {
            var upvote = AnswerUpvotes.FirstOrDefault(a => a.AnswerId == answerId && a.UserId == userId);
            return Task.FromResult(upvote);
        }

        public Task SaveChangesAsync()
        {
            return Task.CompletedTask;
        }

    }
}
