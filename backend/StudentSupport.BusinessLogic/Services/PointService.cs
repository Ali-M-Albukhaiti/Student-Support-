using StudentSupport.BusinessLogic.Services.IServices;
using StudentSupport.DataAccess.Repositories.IRepositories;
using StudentSupport.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentSupport.BusinessLogic.Services
{
    public class PointService : IPointService
    {
        private readonly IPointRepo _pointRepo;

        public PointService(IPointRepo pointRepo)
        {
            _pointRepo = pointRepo;
        }

        public async Task AddPointsAsync(int userId, int points, string reason, string? targetType = null, int? targetId = null)
        {
            var user = await _pointRepo.GetUserByIdAsync(userId) ?? 
                throw new Exception("User not found");

            user.Points += points;
            user.Level = Math.Max(1, (user.Points / 60) + 1);

            var history = new PointHistory
            {
                UserId = userId,
                Points = points,
                Reason = reason,
                TargetType = targetType,
                TargetId = targetId,
                CreatedAt = DateTime.UtcNow
            };

            await _pointRepo.AddPointHistoryAsync(history);
        }

        public async Task<IEnumerable<User>> GetLeaderboardAsync(int top = 10)
        {
            return await _pointRepo.GetTopUsersAsync(top);
        }

        public async Task<IEnumerable<PointHistory>> GetUserHistoryAsync(int userId)
        {
            return await _pointRepo.GetHistoryByUserAsync(userId);
        }
    }
}
