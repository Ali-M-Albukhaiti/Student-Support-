using StudentSupport.BusinessLogic.Services;
using StudentSupport.Models;
using StudentSupport.Tests.FakeRepo;
using System.Threading.Tasks;
using Xunit;

namespace StudentSupport.Tests
{
    public class PostServiceTests
    {
        [Fact]
        public async Task CreatePost_ShouldAddPost_AndAwardPoints()
        {
            // Arrange
            var postRepo = new FakePostRepo();
            var pointRepo = new FakePointRepo();
            pointRepo.Users.Add(new User { Id = 1, UserName = "Ali", Points = 0 }); // Seed user

            var pointService = new PointService(pointRepo);
            var postService = new PostService(postRepo, pointService);

            var newPost = new Post
            {
                AuthorId = 1,
                Title = "Helpful post",
                Content = "Explaining async/await in C#"
            };

            // Act
            var created = await postService.CreatePost(newPost);

            // Assert
            Assert.Single(postRepo.GetAll());
            Assert.Equal(1, created.Id);

            var user = pointRepo.Users.First();
            Assert.Equal(4, user.Points); // ✅ Points added
            Assert.Single(pointRepo.Histories);
            Assert.Equal("Created a new post", pointRepo.Histories[0].Reason);
        }
    }
}
