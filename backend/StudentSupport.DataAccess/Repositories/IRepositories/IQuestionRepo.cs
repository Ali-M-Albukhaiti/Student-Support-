using StudentSupport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupport.DataAccess.Repositories.IRepositories
{
    public interface IQuestionRepo
    {
        Task<IEnumerable<Question>> GetAllQuestionsAsync();
        Task<Question?> GetByIdAsync(int id);
        Task<IEnumerable<Question>> GetByAuthorAsync(int authorId);
        Task AddAsync(Question question);
        Task<Question?> UpdateAsync(Question question);
        Task DeleteAsync(int id);
    }
}
