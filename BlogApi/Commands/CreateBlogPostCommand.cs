using MediatR;
using BlogApi.Models;

namespace BlogApi.Commands
{
    public class CreateBlogPostCommand : IRequest<BlogPost>
    {
        public string Username { get; set; }
        public DateTime DateCreated { get; set; }
        public string Text { get; set; }
    }
}
