using BlogApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogApi.Repositories
{
    public interface IBlogRepository
    {
        Task<IEnumerable<BlogPostResponse>> GetAllAsync();
        Task<BlogPostResponse> GetByEncryptedIdAsync(string encryptedId);
        Task AddAsync(BlogPost blogPost);
        Task UpdateAsync(BlogPost blogPost);
        Task DeleteAsync(string encryptedId);

        Task<int> GetIdByEncryptedIdValAsync(string encryptedId);
    }
}
