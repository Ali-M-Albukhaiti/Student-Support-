using StudentSupport.DataAccess.Repositories.IRepositories;
using StudentSupport.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentSupport.Tests.FakeRepo
{
    public class FakePostRepo : IPostRepo
    {
        private readonly List<Post> _posts = new();

        public Task<IEnumerable<Post>> GetAllPosts()
        {
            return Task.FromResult<IEnumerable<Post>>(_posts);
        }

        public Task<Post?> GetPostById(int id)
        {
            var post = _posts.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(post);
        }

        public Task<Post> AddPost(Post post)
        {
            post.Id = _posts.Count + 1;
            _posts.Add(post);
            return Task.FromResult(post);
        }

        public Task<Post?> UpdatePost(Post post)
        {
            var existing = _posts.FirstOrDefault(p => p.Id == post.Id);
            if (existing != null)
            {
                existing.Title = post.Title;
                existing.Content = post.Content;
                existing.Semester = post.Semester;
                existing.UpdatedAt = post.UpdatedAt;
            }
            return Task.FromResult(existing);
        }

        public Task<bool> DeletePost(int id)
        {
            var post = _posts.FirstOrDefault(p => p.Id == id);
            if (post == null)
                return Task.FromResult(false);

            _posts.Remove(post);
            return Task.FromResult(true);
        }

        // 🧪 Helper for unit tests
        public List<Post> GetAll() => _posts;
    }
}
