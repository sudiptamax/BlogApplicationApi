using MediatR;
using BlogApi.Models;

namespace BlogApi.Queries
{
    public class GetBlogPostByIdQuery : IRequest<BlogPost>
    {
        public int Id { get; set; }
    }
}
