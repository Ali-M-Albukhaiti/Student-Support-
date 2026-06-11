using StudentSupport.BusinessLogic.Services.IServices;
using StudentSupport.DataAccess.Repositories.IRepositories;
using StudentSupport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupport.BusinessLogic.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepo _questionRepo;

        public QuestionService(IQuestionRepo questionRepo)
        {
            _questionRepo = questionRepo;
        }

        public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
        {
           return await _questionRepo.GetAllQuestionsAsync();
        }
            
        public async Task<Question?> GetQuestionByIdAsync(int id)
        {
            return await _questionRepo.GetByIdAsync(id);
        }
            

        public async Task CreateQuestionAsync(Question question)
        {
            await _questionRepo.AddAsync(question);
        }
        public async Task UpdateQuestionAsync(Question question)
        {
            await _questionRepo.UpdateAsync(question);
        }

        public async Task DeleteQuestionAsync(int id)
        {
            await _questionRepo.DeleteAsync(id);
        }
    }
}
