using StudentSupport.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentSupport.BusinessLogic.Services.IServices
{
    public interface IPointService
    {
        Task AddPointsAsync(int userId, int points, string reason, string? targetType = null, int? targetId = null);
        Task<IEnumerable<User>> GetLeaderboardAsync(int top = 10);
        Task<IEnumerable<PointHistory>> GetUserHistoryAsync(int userId);
    }
}
