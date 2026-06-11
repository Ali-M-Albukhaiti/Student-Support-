using StudentSupport.DataAccess.Repositories.IRepositories;
using StudentSupport.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentSupport.Tests.FakeRepo
{
    public class FakePointRepo : IPointRepo
    {
        public List<User> Users { get; } = new();
        public List<PointHistory> Histories { get; } = new();

        public Task<User?> GetUserByIdAsync(int id)
        {
            return Task.FromResult(Users.FirstOrDefault(u => u.Id == id));
        }

        public Task AddPointHistoryAsync(PointHistory history)
        {
            history.Id = Histories.Count + 1;
            Histories.Add(history);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<PointHistory>> GetHistoryByUserAsync(int userId)
        {
            var history = Histories.Where(h => h.UserId == userId);
            return Task.FromResult<IEnumerable<PointHistory>>(history);
        }

        public Task<IEnumerable<User>> GetTopUsersAsync(int top)
        {
            var topUsers = Users.OrderByDescending(u => u.Points).Take(top);
            return Task.FromResult<IEnumerable<User>>(topUsers);
        }
    }
}
