using BlogApi.Commands;
using BlogApi.Models;
using BlogApi.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BlogApi.Handlers
{
    public class CreateBlogPostHandler : IRequestHandler<CreateBlogPostCommand, BlogPost>
    {
        private readonly IBlogRepository _repository;

        public CreateBlogPostHandler(IBlogRepository repository)
        {
            _repository = repository;
        }

        public async Task<BlogPost> Handle(CreateBlogPostCommand request, CancellationToken cancellationToken)
        {
            var blogPost = new BlogPost
            {
                Username = request.Username,
                DateCreated = request.DateCreated,
                Text = request.Text
            };

            await _repository.AddAsync(blogPost);
            return blogPost;
        }
    }
}
