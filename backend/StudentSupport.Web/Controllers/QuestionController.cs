using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentSupport.BusinessLogic.Services.IServices;
using StudentSupport.Models;
using StudentSupport.BusinessLogic.DTOs;
using System.Linq;

namespace StudentSupport.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuestions()
        {
            var questions = await _questionService.GetAllQuestionsAsync();

            // Convert to DTOs with null safety
            var questionDtos = questions.Select(q => new QuestionDto
            {
                Id = q.Id,
                Title = q.Title,
                Content = q.Content,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
                AuthorId = q.AuthorId,
                Author = q.Author == null ? null : new UserDto
                {
                    Id = q.Author.Id,
                    UserName = q.Author.UserName,
                    Email = q.Author.Email,
                    FullName = q.Author.FullName,
                    StudyProgram = q.Author.StudyProgram,
                    Semester = q.Author.Semester,
                    Points = q.Author.Points,
                    Level = q.Author.Level
                }
            }).ToList();

            return Ok(questionDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionById(int id)
        {
            var question = await _questionService.GetQuestionByIdAsync(id);
            if (question == null)
                return NotFound("Question not found");

            // Convert to DTO with null safety
            var questionDto = new QuestionDto
            {
                Id = question.Id,
                Title = question.Title,
                Content = question.Content,
                CreatedAt = question.CreatedAt,
                UpdatedAt = question.UpdatedAt,
                AuthorId = question.AuthorId,
                Author = question.Author == null ? null : new UserDto
                {
                    Id = question.Author.Id,
                    UserName = question.Author.UserName,
                    Email = question.Author.Email,
                    FullName = question.Author.FullName,
                    StudyProgram = question.Author.StudyProgram,
                    Semester = question.Author.Semester,
                    Points = question.Author.Points,
                    Level = question.Author.Level
                },
                Answers = question.Answers?.Select(a => new AnswerDto
                {
                    Id = a.Id,
                    Content = a.Content,
                    CreatedAt = a.CreatedAt,
                    IsBest = a.IsBest,
                    Upvotes = a.Upvotes,
                    AuthorId = a.AuthorId,
                    QuestionId = a.QuestionId,
                    Author = a.Author == null ? null : new UserDto
                    {
                        Id = a.Author.Id,
                        UserName = a.Author.UserName,
                        Email = a.Author.Email,
                        FullName = a.Author.FullName,
                        StudyProgram = a.Author.StudyProgram,
                        Semester = a.Author.Semester,
                        Points = a.Author.Points,
                        Level = a.Author.Level
                    }
                }).ToList() ?? new List<AnswerDto>()
            };

            return Ok(questionDto);
        }

        //[Authorize] // Must be logged in
        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody] Question question)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _questionService.CreateQuestionAsync(question);

            var createdQuestion = await _questionService.GetQuestionByIdAsync(question.Id);

            // Null check for createdQuestion
            if (createdQuestion == null)
                return StatusCode(500, "Failed to retrieve created question");

            // Convert to DTO for response with null safety
            var questionDto = new QuestionDto
            {
                Id = createdQuestion.Id,
                Title = createdQuestion.Title,
                Content = createdQuestion.Content,
                CreatedAt = createdQuestion.CreatedAt,
                UpdatedAt = createdQuestion.UpdatedAt,
                AuthorId = createdQuestion.AuthorId,
                Author = createdQuestion.Author == null ? null : new UserDto
                {
                    Id = createdQuestion.Author.Id,
                    UserName = createdQuestion.Author.UserName,
                    Email = createdQuestion.Author.Email,
                    FullName = createdQuestion.Author.FullName,
                    StudyProgram = createdQuestion.Author.StudyProgram,
                    Semester = createdQuestion.Author.Semester,
                    Points = createdQuestion.Author.Points,
                    Level = createdQuestion.Author.Level
                }
            };

            return CreatedAtAction(nameof(GetQuestionById), new { id = question.Id }, questionDto);
        }

        //[Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(int id, [FromBody] Question question)
        {
            if (id != question.Id)
                return BadRequest("ID mismatch");

            await _questionService.UpdateQuestionAsync(question);
            return NoContent();
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            await _questionService.DeleteQuestionAsync(id);
            return NoContent();
        }
    }
}