using StudentSupport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupport.DataAccess.Repositories.IRepositories
{
    public interface IPostRepo
    {
        Task<IEnumerable<Post>> GetAllPosts();
        Task<Post?> GetPostById(int id);
        Task<Post> AddPost(Post post);
        Task<Post?> UpdatePost(Post post);
        Task<bool> DeletePost(int id);
    }
}
