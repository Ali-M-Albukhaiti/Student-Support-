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
    public class QuestionRepo : IQuestionRepo
    {
        private readonly ApplicationDbContext _db;

        public QuestionRepo(ApplicationDbContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
        {
            return await _db.Questions
                .Include(q => q.Author)
                .Include(q => q.Answers)
                .ToListAsync();
        }

        public async Task<Question?> GetByIdAsync(int id)
        {
            return await _db.Questions
                .Include(q => q.Author)
                .Include(q => q.Answers)
                .ThenInclude(a => a.Author)
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<IEnumerable<Question>> GetByAuthorAsync(int authorId)
        {
            return await _db.Questions
                .Where(q => q.AuthorId == authorId)
                .Include(q => q.Answers)
                .ToListAsync();
        }

        public async Task AddAsync(Question question)
        {
            await _db.Questions.AddAsync(question);
        }
        public async Task <Question?> UpdateAsync(Question question)
        {

            var existing = await _db.Questions.FindAsync(question.Id);
            if (existing == null) throw new InvalidOperationException("Question not found");

            existing.Title = question.Title;
            existing.Content = question.Content;
            existing.UpdatedAt = question.UpdatedAt;

            await _db.SaveChangesAsync();
            return existing;
        }

        public async Task DeleteAsync(int id)
        {
            var question = await _db.Questions.FindAsync(id);
            if (question != null)
            {
                _db.Questions.Remove(question);
                await _db.SaveChangesAsync();
            }
        }

    }
}
