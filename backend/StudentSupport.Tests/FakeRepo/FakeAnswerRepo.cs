using StudentSupport.DataAccess.Repositories.IRepositories;
using StudentSupport.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentSupport.Tests.FakeRepo
{
    public class FakeAnswerRepo : IAnswerRepo
    {
        private readonly List<Answer> _answers = new();

        public Task AddAsync(Answer answer)
        {
            answer.Id = _answers.Count + 1;
            _answers.Add(answer);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var answer = _answers.FirstOrDefault(a => a.Id == id);
            if (answer != null)
                _answers.Remove(answer);
            return Task.CompletedTask;
        }

        public Task<Answer?> GetByIdAsync(int id)
        {
            var answer = _answers.FirstOrDefault(a => a.Id == id);
            return Task.FromResult(answer);
        }

        public Task<IEnumerable<Answer>> GetByQuestionIdAsync(int questionId)
        {
            var answers = _answers.Where(a => a.QuestionId == questionId).AsEnumerable();
            return Task.FromResult(answers);
        }

        public Task UpdateAsync(Answer answer)
        {
            var existing = _answers.FirstOrDefault(a => a.Id == answer.Id);
            if (existing != null)
            {
                existing.Content = answer.Content;
                existing.IsBest = answer.IsBest;
                existing.Upvotes = answer.Upvotes;
                existing.Author = answer.Author;
                existing.QuestionId = answer.QuestionId;
            }
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync()
        {
            return Task.CompletedTask;
        }

        // Helper method for tests
        public List<Answer> GetAll() => _answers;
    }
}
