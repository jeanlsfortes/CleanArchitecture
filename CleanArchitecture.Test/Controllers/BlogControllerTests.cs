using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Domain.Enitites;
using CleanArchitecture.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using Bogus;

namespace CleanArchitecture.Tests
{
    public class BlogControllerTests
    {
        private readonly Mock<IBlogService> _mockBlogService;
        private readonly Mock<ILogger<BlogController>> _mockLogger;
        private readonly BlogController _controller;
        private readonly Faker<Blog> _blogFaker;

        public BlogControllerTests()
        {
            _mockBlogService = new Mock<IBlogService>();
            _mockLogger = new Mock<ILogger<BlogController>>();
            _controller = new BlogController(_mockBlogService.Object, _mockLogger.Object);

            _blogFaker = new Faker<Blog>()
                .CustomInstantiator(f => new Blog(
                    f.Lorem.Sentence(3), // Name
                    f.Lorem.Paragraph(), // Description
                    f.Person.FullName     // Author
                ))
                .RuleFor(b => b.Id, f => Guid.NewGuid())
                .RuleFor(b => b.CreatedAt, f => f.Date.Past())
                .RuleFor(b => b.ImageUrl, f => f.Internet.Avatar());
        }

        private Blog CreateFakeBlog()
        {
            return _blogFaker.Generate(); 
        }

        [Fact]
        public async Task GetAllBlogs_ReturnsOkResult_WithListOfBlogs()
        {
            // Arrange
            var blogs = Enumerable.Range(0, 5).Select(_ => CreateFakeBlog()).ToList(); 
            _mockBlogService.Setup(s => s.GetAllAsync()).ReturnsAsync(blogs);

            // Act
            var result = await _controller.GetAllBlogs();

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(blogs);
        }

        [Fact]
        public async Task GetBlogById_ReturnsOkResult_WhenBlogExists()
        {
            // Arrange
            var blog = CreateFakeBlog();
            _mockBlogService.Setup(s => s.GetByIdAsync(blog.Id)).ReturnsAsync(blog);

            // Act
            var result = await _controller.GetBlogById(blog.Id);

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().Be(blog);
        }

        [Fact]
        public async Task GetBlogById_ReturnsNotFound_WhenBlogDoesNotExist()
        {
            // Arrange
            var blogId = Guid.NewGuid();
            _mockBlogService.Setup(s => s.GetByIdAsync(blogId)).ThrowsAsync(new KeyNotFoundException());

            // Act
            var result = await _controller.GetBlogById(blogId);

            // Assert
            var notFoundResult = result.Result as NotFoundObjectResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task CreateBlog_ReturnsCreatedAtAction_WhenSuccessful()
        {
            // Arrange
            var blogCreateDto = new BlogCreateDto
            {
                Name = _blogFaker.Generate().Name,
                Description = _blogFaker.Generate().Description,
                Author = _blogFaker.Generate().Author
            };

            var createdBlog = new Blog(blogCreateDto.Name, blogCreateDto.Description, blogCreateDto.Author);
            _mockBlogService.Setup(s => s.CreateAsync(blogCreateDto)).ReturnsAsync(createdBlog);

            // Act
            var result = await _controller.CreateBlog(blogCreateDto);

            // Assert
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            createdAtActionResult.Should().NotBeNull();
            createdAtActionResult.StatusCode.Should().Be(201);
            createdAtActionResult.Value.Should().BeEquivalentTo(createdBlog, options =>
                options.Excluding(b => b.Id)); 
        }

        [Fact]
        public async Task UpdateBlog_ReturnsOkResult_WhenSuccessful()
        {
            // Arrange
            var blogId = Guid.NewGuid();
            var blogUpdateDto = new BlogUpdateDto
            {
                Id = blogId,
                Name = _blogFaker.Generate().Name,
                Description = _blogFaker.Generate().Description,
                Author = _blogFaker.Generate().Author
            };

            var updatedBlog = new Blog(blogUpdateDto.Name, blogUpdateDto.Description, blogUpdateDto.Author);
            typeof(Blog).GetProperty(nameof(Blog.Id))?.SetValue(updatedBlog, blogId);
            _mockBlogService.Setup(s => s.UpdateAsync(blogUpdateDto)).ReturnsAsync(updatedBlog);

            // Act
            var result = await _controller.UpdateBlog(blogId, blogUpdateDto);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(updatedBlog, options =>
                options.Excluding(b => b.Id));
        }

        [Fact]
        public async Task DeleteBlog_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            var blogId = Guid.NewGuid();
            _mockBlogService.Setup(s => s.DeleteAsync(blogId)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteBlog(blogId);

            // Assert
            var noContentResult = result as NoContentResult;
            noContentResult.Should().NotBeNull();
            noContentResult.StatusCode.Should().Be(204);
        }
    }
}
