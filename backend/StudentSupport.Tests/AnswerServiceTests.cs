using StudentSupport.BusinessLogic.Services;
using StudentSupport.Models;
using StudentSupport.Tests.FakeRepo;
using Xunit;
using System.Threading.Tasks;
using System.Linq;

namespace StudentSupport.Tests
{
    public class AnswerServiceTests
    {
        [Fact]
        public async Task UpvoteAnswerAsync_ShouldToggleUpvotes()
        {
            // Arrange
            var answerRepo = new FakeAnswerRepo();
            var upvoteRepo = new FakeAnswerUpvoteRepo();
            var pointRepo = new FakePointRepo();

            var pointService = new PointService(pointRepo);
            var service = new AnswerService(answerRepo, upvoteRepo, pointService);

            var answer = new Answer { Content = "This is a test answer", QuestionId = 1, Upvotes = 0 };
            await answerRepo.AddAsync(answer);

            int testUserId = 1;

            // Act: Add upvote
            await service.UpvoteAnswerAsync(answer.Id, testUserId);

            // Assert: Upvote added
            var updated = await answerRepo.GetByIdAsync(answer.Id);
            Assert.Equal(1, updated!.Upvotes);

            // Act: Remove upvote
            await service.UpvoteAnswerAsync(answer.Id, testUserId);

            // Assert: Upvote removed
            var updatedAgain = await answerRepo.GetByIdAsync(answer.Id);
            Assert.Equal(0, updatedAgain!.Upvotes);
        }

        [Fact]
        public async Task MarkAsBestAnswerAsync_ShouldMarkOnlyOneAnswer()
        {
            // Arrange
            var answerRepo = new FakeAnswerRepo();
            var upvoteRepo = new FakeAnswerUpvoteRepo();
            var pointRepo = new FakePointRepo();
            // required for constructor
            var pointService = new PointService(pointRepo);
            var service = new AnswerService(answerRepo, upvoteRepo, pointService);

            var answer1 = new Answer { Content = "Answer 1", QuestionId = 10 };
            var answer2 = new Answer { Content = "Answer 2", QuestionId = 10 };

            await answerRepo.AddAsync(answer1);
            await answerRepo.AddAsync(answer2);

            // Act
            await service.MarkAsBestAnswerAsync(answer2.Id);

            // Assert
            var allAnswers = await answerRepo.GetByQuestionIdAsync(10);
            Assert.Single(allAnswers.Where(a => a.IsBest)); // only one should be best
            Assert.True(allAnswers.First(a => a.Id == answer2.Id).IsBest);

        }

        [Fact]
        public async Task AddAnswerAsync_ShouldAddAnswer_AndAwardPoints()
        {
            // Arrange
            var answerRepo = new FakeAnswerRepo();
            var upvoteRepo = new FakeAnswerUpvoteRepo();
            var pointRepo = new FakePointRepo();

            // Seed a user to test point increment
            pointRepo.Users.Add(new User { Id = 1, UserName = "Ali", Points = 0 });

            var pointService = new PointService(pointRepo);
            var service = new AnswerService(answerRepo, upvoteRepo, pointService);

            var answer = new Answer
            {
                Content = "New Answer",
                QuestionId = 5,
                AuthorId = 1
            };

            // Act
            await service.AddAnswerAsync(answer);

            // Assert
            var added = await answerRepo.GetByIdAsync(answer.Id);
            Assert.NotNull(added);
            Assert.Equal("New Answer", added!.Content);

            // ✅ Check that points were added
            var user = pointRepo.Users.First();
            Assert.Equal(2, user.Points);
            Assert.Single(pointRepo.Histories);
            Assert.Equal("Posted an answer", pointRepo.Histories[0].Reason);
        }

    }
}
