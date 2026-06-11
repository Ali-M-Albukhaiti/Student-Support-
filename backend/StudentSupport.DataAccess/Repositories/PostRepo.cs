using Microsoft.EntityFrameworkCore;
using StudentSupport.DataAccess.Data;
using StudentSupport.DataAccess.Repositories.IRepositories;
using StudentSupport.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentSupport.DataAccess.Repositories
{
    public class PostRepo : IPostRepo
    {
        private readonly ApplicationDbContext _db;

        public PostRepo(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            return await _db.Posts
                            .Include(p => p.Author)
                            .ToListAsync();
        }

        public async Task<Post?> GetPostById(int id)
        {
            return await _db.Posts
                            .Include(p => p.Author)
                            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Post> AddPost(Post post)
        {
            await _db.Posts.AddAsync(post);
            await _db.SaveChangesAsync();
            return post;
        }

        public async Task<Post?> UpdatePost(Post post)
        {
            var existing = await _db.Posts.FindAsync(post.Id);
            if (existing == null) return null;

            existing.Title = post.Title;
            existing.Content = post.Content;
            existing.Semester = post.Semester;
            existing.UpdatedAt = post.UpdatedAt;

            await _db.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeletePost(int id)
        {
            var post = await _db.Posts.FindAsync(id);
            if (post == null) return false;

            _db.Posts.Remove(post);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
