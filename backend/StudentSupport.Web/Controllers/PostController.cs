using Microsoft.AspNetCore.Mvc;
using StudentSupport.BusinessLogic.Services.IServices;
using StudentSupport.Models;
using StudentSupport.BusinessLogic.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace StudentSupport.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetAll()
        {
            var posts = await _postService.GetAllPosts();

            // Convert to DTOs to hide sensitive data
            var postDtos = posts.Select(p => new PostDto
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                Semester = p.Semester,
                AuthorId = p.AuthorId,
                Author = p.Author != null ? new UserDto
                {
                    Id = p.Author.Id,
                    UserName = p.Author.UserName,
                    Email = p.Author.Email,
                    FullName = p.Author.FullName,
                    StudyProgram = p.Author.StudyProgram,
                    Semester = p.Author.Semester,
                    Points = p.Author.Points,
                    Level = p.Author.Level
                } : null
            }).ToList();

            return Ok(postDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> GetById(int id)
        {
            var post = await _postService.GetPostById(id);
            if (post == null) return NotFound();

            // Convert to DTO
            var postDto = new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreatedAt = post.CreatedAt,
                UpdatedAt = post.UpdatedAt,
                Semester = post.Semester,
                AuthorId = post.AuthorId,
                Author = post.Author != null ? new UserDto
                {
                    Id = post.Author.Id,
                    UserName = post.Author.UserName,
                    Email = post.Author.Email,
                    FullName = post.Author.FullName,
                    StudyProgram = post.Author.StudyProgram,
                    Semester = post.Author.Semester,
                    Points = post.Author.Points,
                    Level = post.Author.Level
                } : null
            };

            return Ok(postDto);
        }

        [HttpPost]
        public async Task<ActionResult<PostDto>> CreatePost(Post post)
        {
            // Still accept the full Post model for creation
            var createdPost = await _postService.CreatePost(post);

            // But return DTO in response
            var postDto = new PostDto
            {
                Id = createdPost.Id,
                Title = createdPost.Title,
                Content = createdPost.Content,
                CreatedAt = createdPost.CreatedAt,
                UpdatedAt = createdPost.UpdatedAt,
                Semester = createdPost.Semester,
                AuthorId = createdPost.AuthorId,
                Author = createdPost.Author != null ? new UserDto
                {
                    Id = createdPost.Author.Id,
                    UserName = createdPost.Author.UserName,
                    Email = createdPost.Author.Email,
                    FullName = createdPost.Author.FullName,
                    StudyProgram = createdPost.Author.StudyProgram,
                    Semester = createdPost.Author.Semester,
                    Points = createdPost.Author.Points,
                    Level = createdPost.Author.Level
                } : null
            };

            return CreatedAtAction(nameof(GetById), new { id = createdPost.Id }, postDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PostDto>> UpdatePost(int id, Post post)
        {
            if (id != post.Id) return BadRequest();
            var updatedPost = await _postService.UpdatePost(id, post);
            if (updatedPost == null) return NotFound();

            // Return DTO
            var postDto = new PostDto
            {
                Id = updatedPost.Id,
                Title = updatedPost.Title,
                Content = updatedPost.Content,
                CreatedAt = updatedPost.CreatedAt,
                UpdatedAt = updatedPost.UpdatedAt,
                Semester = updatedPost.Semester,
                AuthorId = updatedPost.AuthorId,
                Author = updatedPost.Author != null ? new UserDto
                {
                    Id = updatedPost.Author.Id,
                    UserName = updatedPost.Author.UserName,
                    Email = updatedPost.Author.Email,
                    FullName = updatedPost.Author.FullName,
                    StudyProgram = updatedPost.Author.StudyProgram,
                    Semester = updatedPost.Author.Semester,
                    Points = updatedPost.Author.Points,
                    Level = updatedPost.Author.Level
                } : null
            };

            return Ok(postDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(int id)
        {
            var result = await _postService.DeletePost(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}