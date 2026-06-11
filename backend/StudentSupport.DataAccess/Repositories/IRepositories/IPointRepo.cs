using StudentSupport.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentSupport.DataAccess.Repositories.IRepositories
{
    public interface IPointRepo
    {
        Task<User?> GetUserByIdAsync(int id);
        Task AddPointHistoryAsync(PointHistory history);
        Task<IEnumerable<PointHistory>> GetHistoryByUserAsync(int userId);
        Task<IEnumerable<User>> GetTopUsersAsync(int count = 10);
    }
}
