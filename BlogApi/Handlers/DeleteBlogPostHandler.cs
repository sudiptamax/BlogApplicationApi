using BlogApi.Commands;
using BlogApi.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BlogApi.Handlers
{
    public class DeleteBlogPostHandler : IRequestHandler<DeleteBlogPostCommand, Unit>
    {
        private readonly IBlogRepository _repository;

        public DeleteBlogPostHandler(IBlogRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteBlogPostCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.EncryptedId);
            return Unit.Value;
        }
    }
}
