using Microsoft.EntityFrameworkCore;
using StudentSupport.DataAccess.Data;
using StudentSupport.DataAccess.Repositories.IRepositories;
using StudentSupport.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentSupport.DataAccess.Repositories
{
    public class AnswerUpvoteRepo : IAnswerUpvoteRepo
    {
        private readonly ApplicationDbContext _db;

        public AnswerUpvoteRepo(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<AnswerUpvote?> GetByUserAndAnswerAsync(int userId, int answerId)
        {
            return await _db.AnswerUpvotes
                .FirstOrDefaultAsync(v => v.AnswerId == answerId && v.UserId == userId);
        }

        public async Task<AnswerUpvote> AddAsync(AnswerUpvote upvote)
        {
            await _db.AnswerUpvotes.AddAsync(upvote);
            await _db.SaveChangesAsync();
            return upvote;
        }

        public async Task RemoveAsync(AnswerUpvote upvote)
        {
            _db.AnswerUpvotes.Remove(upvote);
            await _db.SaveChangesAsync();
        }

        public async Task<int> CountByAnswerIdAsync(int answerId)
        {
            return await _db.AnswerUpvotes
                .CountAsync(v => v.AnswerId == answerId);
        }

    }
}
