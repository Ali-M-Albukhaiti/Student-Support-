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
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpGet("question/{questionId}")]
        public async Task<IActionResult> GetAnswersByQuestion(int questionId)
        {
            var answers = await _answerService.GetAnswersByQuestionAsync(questionId);

            // Convert to DTOs with null safety
            var answerDtos = answers.Select(a => new AnswerDto
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
            }).ToList();

            return Ok(answerDtos);
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> AddAnswer([FromBody] Answer answer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _answerService.AddAnswerAsync(answer);

            // Get the created answer with null safety
            var createdAnswer = await _answerService.GetByIdAsync(answer.Id);

            if (createdAnswer == null)
                return StatusCode(500, "Failed to retrieve created answer");

            // Convert to DTO for response with null safety
            var answerDto = new AnswerDto
            {
                Id = createdAnswer.Id,
                Content = createdAnswer.Content,
                CreatedAt = createdAnswer.CreatedAt,
                IsBest = createdAnswer.IsBest,
                Upvotes = createdAnswer.Upvotes,
                AuthorId = createdAnswer.AuthorId,
                QuestionId = createdAnswer.QuestionId,
                Author = createdAnswer.Author == null ? null : new UserDto
                {
                    Id = createdAnswer.Author.Id,
                    UserName = createdAnswer.Author.UserName,
                    Email = createdAnswer.Author.Email,
                    FullName = createdAnswer.Author.FullName,
                    StudyProgram = createdAnswer.Author.StudyProgram,
                    Semester = createdAnswer.Author.Semester,
                    Points = createdAnswer.Author.Points,
                    Level = createdAnswer.Author.Level
                }
            };

            return Ok(answerDto);
        }

        //[Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnswer(int id, [FromBody] Answer answer)
        {
            if (id != answer.Id)
                return BadRequest("ID mismatch");

            await _answerService.UpdateAnswerAsync(answer);
            return NoContent();
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnswer(int id)
        {
            await _answerService.DeleteAnswerAsync(id);
            return NoContent();
        }

        //[Authorize]
        [HttpPost("{answerId}/upvote")]
        public async Task<IActionResult> UpvoteAnswer(int answerId, [FromQuery] int userId)
        {
            // Call the service to toggle the upvote
            var toggled = await _answerService.UpvoteAnswerAsync(answerId, userId);

            // After toggling, fetch the updated answer with null safety
            var updatedAnswer = await _answerService.GetByIdAsync(answerId);

            if (updatedAnswer == null)
                return NotFound(new { message = "Answer not found." });

            // Convert to DTO for response with null safety
            var answerDto = new AnswerDto
            {
                Id = updatedAnswer.Id,
                Content = updatedAnswer.Content,
                CreatedAt = updatedAnswer.CreatedAt,
                IsBest = updatedAnswer.IsBest,
                Upvotes = updatedAnswer.Upvotes,
                AuthorId = updatedAnswer.AuthorId,
                QuestionId = updatedAnswer.QuestionId,
                Author = updatedAnswer.Author == null ? null : new UserDto
                {
                    Id = updatedAnswer.Author.Id,
                    UserName = updatedAnswer.Author.UserName,
                    Email = updatedAnswer.Author.Email,
                    FullName = updatedAnswer.Author.FullName,
                    StudyProgram = updatedAnswer.Author.StudyProgram,
                    Semester = updatedAnswer.Author.Semester,
                    Points = updatedAnswer.Author.Points,
                    Level = updatedAnswer.Author.Level
                }
            };

            // Return both the updated answer and a short message
            return Ok(new
            {
                message = toggled ? "Answer upvoted successfully!" : "Upvote removed.",
                answer = answerDto
            });
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut("{id}/mark-best")]
        public async Task<IActionResult> MarkAsBest(int id)
        {
            await _answerService.MarkAsBestAnswerAsync(id);
            return Ok("Best answer marked successfully.");
        }
    }
}