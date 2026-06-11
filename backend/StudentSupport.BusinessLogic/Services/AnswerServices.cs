using StudentSupport.BusinessLogic.Services.IServices;
using StudentSupport.DataAccess.Repositories.IRepositories;
using StudentSupport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupport.BusinessLogic.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepo _answerRepo;
        private readonly IAnswerUpvoteRepo _upvoteRepo;
        private readonly IPointService _pointService;

        public AnswerService(IAnswerRepo answerRepo, IAnswerUpvoteRepo upvoteRepo, IPointService pointService)
        {
            _answerRepo = answerRepo;
            _upvoteRepo = upvoteRepo;
            _pointService = pointService;
        }

        public async Task<IEnumerable<Answer>> GetAnswersByQuestionAsync(int questionId)
        {
           return await _answerRepo.GetByQuestionIdAsync(questionId);
        }
            

        public async Task AddAnswerAsync(Answer answer)
        {
            await _answerRepo.AddAsync(answer);

            await _pointService.AddPointsAsync(answer.AuthorId, 2, "Posted an answer");

        }
        public async Task<Answer?> GetByIdAsync(int answerId)
        {
            return await _answerRepo.GetByIdAsync(answerId);
        }
        public async Task UpdateAnswerAsync(Answer answer)
        {
            await _answerRepo.UpdateAsync(answer);
        }

        public async Task MarkAsBestAnswerAsync(int answerId)
        {
            var answer = await _answerRepo.GetByIdAsync(answerId);
            if (answer != null)
            {
                // Unmark others
                var allAnswers = await _answerRepo.GetByQuestionIdAsync(answer.QuestionId);
                foreach (var a in allAnswers)
                    a.IsBest = a.Id == answerId;

                await _answerRepo.SaveChangesAsync();
            }
        }

        public async Task<bool> UpvoteAnswerAsync(int answerId, int userId)
        {
            var existingUpvote = await _upvoteRepo.GetByUserAndAnswerAsync(userId, answerId);
            var answer = await _answerRepo.GetByIdAsync(answerId);
            if (answer == null) return false;

            if (existingUpvote != null)
            {
                await _upvoteRepo.RemoveAsync(existingUpvote);
                answer.Upvotes = await _upvoteRepo.CountByAnswerIdAsync(answerId);
                await _answerRepo.UpdateAsync(answer);
                await _answerRepo.SaveChangesAsync();
                return false; // removed
            }

            var upvote = new AnswerUpvote
            {
                AnswerId = answerId,
                UserId = userId
            };

            await _upvoteRepo.AddAsync(upvote);
            answer.Upvotes = await _upvoteRepo.CountByAnswerIdAsync(answerId);
            await _answerRepo.UpdateAsync(answer);
            await _answerRepo.SaveChangesAsync();
            return true; // added
        }

        public async Task DeleteAnswerAsync(int id)
        {
            await _answerRepo.DeleteAsync(id);
        }
            
    }
}
