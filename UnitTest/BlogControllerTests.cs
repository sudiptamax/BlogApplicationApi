using System.Threading;
using System.Threading.Tasks;
using BlogApi.Commands;
using BlogApi.Controllers;
using BlogApi.Models;
using BlogApi.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BlogApi.Tests.Controllers
{
    public class BlogControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly BlogController _controller;


        #region UnitTest
        public BlogControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new BlogController(_mediatorMock.Object);
        }

        [Fact]
        public async Task CreateBlog_ShouldReturnCreatedAtAction()
        {
            // Arrange
            var blog = new CreateBlogPostCommand { Username = "User1", DateCreated = DateTime.Now, Text = "Blog 1" };


            // Act
            var result = await _controller.CreateBlogPost(blog);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetBlogById", actionResult.ActionName);
            Assert.Equal(blog.Username, actionResult.RouteValues["id"]);
        }

        [Fact]
        public async Task GetBlogById_ShouldReturnBlog()
        {
            // Arrange
            var blog = new BlogPost { Username = "User1", DateCreated = DateTime.Now, Text = "Blog 1" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetBlogPostByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(blog);

            // Act
            var result = await _controller.GetBlogPostById("1");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedBlog = Assert.IsType<BlogPost>(okResult.Value);
            Assert.Equal("1", returnedBlog.EncryptedId);
        }

        #endregion



    }
}


