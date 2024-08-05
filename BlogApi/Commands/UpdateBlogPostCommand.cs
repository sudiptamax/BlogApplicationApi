using MediatR;
using BlogApi.Models;

namespace BlogApi.Commands
{
    public class UpdateBlogPostCommand : IRequest<BlogPost>
    {
        public string id { get; set; }
        public string Username { get; set; }
        public DateTime DateCreated { get; set; }
        public string Text { get; set; }
    }
}
