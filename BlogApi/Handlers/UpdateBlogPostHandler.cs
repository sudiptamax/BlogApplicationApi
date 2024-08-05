using BlogApi.Commands;
using BlogApi.Models;
using BlogApi.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BlogApi.Handlers
{
    public class UpdateBlogPostHandler : IRequestHandler<UpdateBlogPostCommand, BlogPost>
    {
        private readonly IBlogRepository _repository;

        public UpdateBlogPostHandler(IBlogRepository repository)
        {
            _repository = repository;
        }

        public async Task<BlogPost> Handle(UpdateBlogPostCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the original ID using the encrypted ID
            int originalId = await _repository.GetIdByEncryptedIdValAsync(request.id);

            var blogPost = new BlogPost
            {
                Id = originalId,
                Username = request.Username,
                DateCreated = request.DateCreated,
                Text = request.Text
            };

            await _repository.UpdateAsync(blogPost);
            return blogPost;
        }
    }
}
