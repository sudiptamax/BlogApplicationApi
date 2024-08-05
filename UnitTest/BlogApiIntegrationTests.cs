using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlogApi.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace BlogApi.Tests.IntegrationTests
{
    public class BlogApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public BlogApiIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetBlogs_ShouldReturnBlogs()
        {
            var response = await _client.GetAsync("/api/Blog");
            response.EnsureSuccessStatusCode();

            var blogs = await response.Content.ReadFromJsonAsync<BlogPost[]>();
            Assert.NotNull(blogs);
            Assert.NotEmpty(blogs);
        }

        [Fact]
        public async Task CreateBlog_ShouldReturnCreatedBlog()
        {
            var newBlog = new BlogPost { Username = "Test User", DateCreated = DateTime.Now, Text = "Test Blog" };

            var response = await _client.PostAsJsonAsync("/api/Blog", newBlog);
            response.EnsureSuccessStatusCode();

            var createdBlog = await response.Content.ReadFromJsonAsync<BlogPost>();
            Assert.Equal(newBlog.Username, createdBlog.Username);
        }
    }
}
