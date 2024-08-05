using BlogApi.Models;
using MediatR;

namespace BlogApi.Queries
{
    public class GetBlogPostByEncryptedIdQuery : IRequest<BlogPostResponse>
    {
        public string EncryptedId { get; set; }
    }
}
