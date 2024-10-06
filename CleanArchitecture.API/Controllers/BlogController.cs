using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Enitites;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly ILogger<BlogController> _logger;

        public BlogController(IBlogService blogService, ILogger<BlogController> logger)
        {
            _blogService = blogService ?? throw new ArgumentNullException(nameof(blogService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<List<Blog>>> GetAllBlogs()
        {
            try
            {
                var blogs = await _blogService.GetAllAsync();
                return Ok(blogs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all blogs.");
                return StatusCode(500, "An error occurred while retrieving the blogs.");
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Blog>> GetBlogById(Guid id)
        {
            try
            {
                var blog = await _blogService.GetByIdAsync(id);
                return Ok(blog);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Blog with ID {Id} not found.", id);
                return NotFound($"Blog with ID {id} not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving blog with ID {Id}.", id);
                return StatusCode(500, "An error occurred while retrieving the blog.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Blog>> CreateBlog([FromBody] BlogCreateDto blog)
        {
            if (blog == null)
            {
                return BadRequest("Blog object is null.");
            }

            try
            {
                var createdBlog = await _blogService.CreateAsync(blog);
                return CreatedAtAction(nameof(GetBlogById), new { id = createdBlog.Id }, createdBlog);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating blog.");
                return StatusCode(500, "An error occurred while creating the blog.");
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateBlog(Guid id, [FromBody] BlogUpdateDto blog)
        {
            if (id != blog.Id)
            {
                return BadRequest("Blog ID mismatch.");
            }

            try
            {
                var updatedBlog = await _blogService.UpdateAsync(blog);
                return Ok(updatedBlog);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Blog with ID {Id} not found.", id);
                return NotFound($"Blog with ID {id} not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating blog with ID {Id}.", id);
                return StatusCode(500, "An error occurred while updating the blog.");
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteBlog(Guid id)
        {
            try
            {
                var result = await _blogService.DeleteAsync(id);
                if (!result)
                {
                    return NotFound($"Blog with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting blog with ID {Id}.", id);
                return StatusCode(500, "An error occurred while deleting the blog.");
            }
        }
    }
}
