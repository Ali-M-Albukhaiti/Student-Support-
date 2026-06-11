using StudentSupport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupport.BusinessLogic.Services.IServices
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetAllPosts();
        Task<Post?> GetPostById(int id);
        Task<Post> CreatePost(Post post);
        Task<Post?> UpdatePost(int id, Post post);
        Task<bool> DeletePost(int id);
    }
}
