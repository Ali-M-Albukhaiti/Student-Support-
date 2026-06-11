using Microsoft.EntityFrameworkCore;
using StudentSupport.DataAccess.Data;
using StudentSupport.DataAccess.Repositories.IRepositories;
using StudentSupport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupport.DataAccess.Repositories
{
    public class AnswerRepo : IAnswerRepo
    {
        private readonly ApplicationDbContext _db;

        public AnswerRepo(ApplicationDbContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<Answer>> GetByQuestionIdAsync(int questionId)
        {
            return await _db.Answers
                .Where(a => a.QuestionId == questionId)
                .Include(a => a.Author)
                .Include(a => a.Question)
                .ToListAsync();
        }

        public async Task<Answer?> GetByIdAsync(int id)
        {
            return await _db.Answers
                .Include(a => a.Author)
                .Include(a => a.Question)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(Answer answer)
        {
            await _db.Answers.AddAsync(answer);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(Answer answer)
        {
            _db.Answers.Update(answer);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var answer = await _db.Answers.FindAsync(id);
            if (answer != null)
            {
                _db.Answers.Remove(answer);
                await SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
    }
}
