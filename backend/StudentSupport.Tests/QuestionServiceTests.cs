using StudentSupport.BusinessLogic.Services;
using StudentSupport.Models;
using StudentSupport.Tests.FakeRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupport.Tests
{
    public class QuestionServiceTests
    {
        private readonly QuestionService _service;

        public QuestionServiceTests()
        {
            var fakeRepo = new FakeQuestionRepo();
            _service = new QuestionService(fakeRepo);
        }

        [Fact]
        public async Task CreateQuestion_ShouldAddQuestion()
        {
            // Arrange
            var question = new Question { Title = "Test Q1", Content = "This is a test", AuthorId = 1 };

            // Act
            await _service.CreateQuestionAsync(question);
            var allQuestions = await _service.GetAllQuestionsAsync();

            // Assert
            Assert.Single(allQuestions);
            Assert.Equal("Test Q1", allQuestions.First().Title);
        }

        [Fact]
        public async Task DeleteQuestion_ShouldRemoveQuestion()
        {
            // Arrange
            var question = new Question { Title = "Delete Test", Content = "Delete me", AuthorId = 1 };
            await _service.CreateQuestionAsync(question);

            // Act
            await _service.DeleteQuestionAsync(question.Id);
            var allQuestions = await _service.GetAllQuestionsAsync();

            // Assert
            Assert.Empty(allQuestions);
        }
    }
}
