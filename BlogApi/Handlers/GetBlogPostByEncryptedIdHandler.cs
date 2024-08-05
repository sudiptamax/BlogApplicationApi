using BlogApi.Models;
using BlogApi.Queries;
using BlogApi.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BlogApi.Handlers
{
    public class GetBlogPostByEncryptedIdHandler : IRequestHandler<GetBlogPostByEncryptedIdQuery, BlogPostResponse>
    {
        private readonly IBlogRepository _repository;

        public GetBlogPostByEncryptedIdHandler(IBlogRepository repository)
        {
            _repository = repository;
        }

        public async Task<BlogPostResponse> Handle(GetBlogPostByEncryptedIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByEncryptedIdAsync(request.EncryptedId);
        }
    }
}
