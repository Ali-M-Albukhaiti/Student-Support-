using StudentSupport.DataAccess.Repositories.IRepositories;
using StudentSupport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupport.Tests.FakeRepo
{
    public class FakeQuestionRepo : IQuestionRepo
    {
        private readonly List<Question> _questions = new();

        public Task<IEnumerable<Question>> GetAllQuestionsAsync()
        {
            return Task.FromResult<IEnumerable<Question>>(_questions);
        }

        public Task<Question?> GetByIdAsync(int id)
        {
            return Task.FromResult(_questions.FirstOrDefault(q => q.Id == id));
        }

        public Task AddAsync(Question question)
        {
            question.Id = _questions.Count + 1;
            _questions.Add(question);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Question question)
        {
            var q = _questions.FirstOrDefault(x => x.Id == question.Id);
            if (q != null)
            {
                q.Title = question.Title;
                q.Content = question.Content;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            _questions.RemoveAll(q => q.Id == id);
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync() => Task.CompletedTask;

        public Task<IEnumerable<Question>> GetByAuthorAsync(int authorId)
        {
            throw new NotImplementedException();
        }

        Task<Question?> IQuestionRepo.UpdateAsync(Question question)
        {
            throw new NotImplementedException();
        }
    }
}
