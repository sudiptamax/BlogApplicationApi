using MediatR;

namespace BlogApi.Commands
{
    public class DeleteBlogPostCommand : IRequest<Unit>
    {
        public string EncryptedId { get; set; }
    }
}
