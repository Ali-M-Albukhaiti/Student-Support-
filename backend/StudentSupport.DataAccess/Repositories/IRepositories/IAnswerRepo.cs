using StudentSupport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupport.DataAccess.Repositories.IRepositories
{
    public interface IAnswerRepo
    {
        Task<IEnumerable<Answer>> GetByQuestionIdAsync(int questionId);
        Task<Answer?> GetByIdAsync(int id);
        Task AddAsync(Answer answer);
        Task UpdateAsync(Answer answer);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
