using Microsoft.EntityFrameworkCore;
using StudentSupport.DataAccess.Data;
using StudentSupport.DataAccess.Repositories.IRepositories;
using StudentSupport.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentSupport.DataAccess.Repositories
{
    public class PointRepo : IPointRepo
    {
        private readonly ApplicationDbContext _db;

        public PointRepo(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task AddPointHistoryAsync(PointHistory history)
        {
            await _db.PointHistories.AddAsync(history);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<PointHistory>> GetHistoryByUserAsync(int userId)
        {
            return await _db.PointHistories
                .Where(ph => ph.UserId == userId)
                .OrderByDescending(ph => ph.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetTopUsersAsync(int count = 10)
        {
            return await _db.Users
                .OrderByDescending(u => u.Points)
                .Take(count)
                .ToListAsync();
        }
    }
}
