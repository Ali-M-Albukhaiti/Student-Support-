using StudentSupport.BusinessLogic.Services.IServices;
using StudentSupport.DataAccess.Repositories.IRepositories;
using StudentSupport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentSupport.BusinessLogic.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepo _postRepo;
        private readonly IPointService _pointService;

        public PostService(IPostRepo postRepo, IPointService pointService)
        {
            _postRepo = postRepo;
            _pointService = pointService;
        }

        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            var posts = await _postRepo.GetAllPosts();
            return posts.OrderByDescending(p => p.CreatedAt); // Newest first
        }

        public async Task<Post?> GetPostById(int id)
        {
            return await _postRepo.GetPostById(id);
        }

        public async Task<Post> CreatePost(Post post)
        {
            // ✅ Business validation
            if (string.IsNullOrWhiteSpace(post.Title))
                throw new ArgumentException("Title cannot be empty.");

            if (string.IsNullOrWhiteSpace(post.Content))
                throw new ArgumentException("Content cannot be empty.");

            // ✅ Database will handle the ID auto-increment
            post.CreatedAt = DateTime.UtcNow;
            post.UpdatedAt = null;

            var Created = await _postRepo.AddPost(post);

            // ✅ Award points to the author for creating a post
            await _pointService.AddPointsAsync(post.AuthorId, 4, "Created a new post");
            return Created;
        }

        public async Task<Post?> UpdatePost(int id, Post post)
        {
            var existing = await _postRepo.GetPostById(id);
            if (existing == null)
                return null;

            // ✅ Update only modified fields
            existing.Title = !string.IsNullOrWhiteSpace(post.Title) ? post.Title : existing.Title;
            existing.Content = !string.IsNullOrWhiteSpace(post.Content) ? post.Content : existing.Content;
            existing.Semester = post.Semester;
            existing.UpdatedAt = DateTime.UtcNow;

            return await _postRepo.UpdatePost(existing);
        }

        public async Task<bool> DeletePost(int id)
        {
            var existing = await _postRepo.GetPostById(id);
            if (existing == null)
                return false;

            // ✅ Prevent deleting posts with comments
            if (existing.Comments != null && existing.Comments.Any())
                throw new InvalidOperationException("Cannot delete a post that has comments.");

            return await _postRepo.DeletePost(id);
        }
    }
}
