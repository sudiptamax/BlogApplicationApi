using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApi.Models;
using BlogApi.Repositories;
using Moq;
using Xunit;

namespace BlogApi.Tests.Services
{
    public class BlogServiceTests
    {
        private readonly Mock<IBlogRepository> _blogRepositoryMock;
        private readonly BlogRepository _blogService;

        public BlogServiceTests()
        {
            _blogRepositoryMock = new Mock<IBlogRepository>();
            _blogService = new BlogRepository();
        }

        [Fact]
        public async Task GetAllBlogs_ShouldReturnBlogs()
        {
            // Arrange
            var mockBlogs = new List<BlogPostResponse>
            {
                new BlogPostResponse { Username = "User1", DateCreated = DateTime.Now, Text = "Blog 1" },
                new BlogPostResponse { Username = "User2", DateCreated = DateTime.Now, Text = "Blog 2" }
            };
            _blogRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(mockBlogs);

            // Act
            var result = await _blogService.GetAllAsync();

            // Assert
            Assert.Equal(2.ToString(), result.Count().ToString());
            Assert.Equal("Blog 1", result.First().ToString());
        }
    }
}
