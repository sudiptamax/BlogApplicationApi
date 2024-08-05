using BlogApi.Models;
using BlogApi.Repositories;
using Newtonsoft.Json;

public class BlogRepository : IBlogRepository
{
    private readonly string _filePath = "Data/blogPosts.json";
    private List<BlogPost> _blogPosts;

    public BlogRepository()
    {
        var directory = Path.GetDirectoryName(_filePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        if (File.Exists(_filePath))
        {
            var jsonData = File.ReadAllText(_filePath);
            _blogPosts = JsonConvert.DeserializeObject<List<BlogPost>>(jsonData) ?? new List<BlogPost>();
        }
        else
        {
            _blogPosts = new List<BlogPost>();
            SaveToFileAsync().Wait();
        }
    }

    private Task SaveToFileAsync()
    {
        var jsonData = JsonConvert.SerializeObject(_blogPosts, Formatting.Indented);
        return File.WriteAllTextAsync(_filePath, jsonData);
    }

    public async Task<IEnumerable<BlogPostResponse>> GetAllAsync()
    {
        var response = _blogPosts.Select(b => new BlogPostResponse
        {
            Id = b.EncryptedId,
            Username = b.Username,
            DateCreated = b.DateCreated,
            Text = b.Text
        });

        return await Task.FromResult(response);
    }

    public async Task<BlogPostResponse> GetByEncryptedIdAsync(string encryptedId)
    {
        var blogPost = _blogPosts.FirstOrDefault(b => b.EncryptedId == encryptedId);
        if (blogPost == null) return null;

        var response = new BlogPostResponse
        {
            Id = blogPost.EncryptedId,
            Username = blogPost.Username,
            DateCreated = blogPost.DateCreated,
            Text = blogPost.Text
        };

        return await Task.FromResult(response);
    }


    public async Task AddAsync(BlogPost blogPost)
    {
        blogPost.Id = _blogPosts.Any() ? _blogPosts.Max(b => b.Id) + 1 : 1;
        blogPost.EncryptedId = EncryptionHelper.EncryptId(blogPost.Id);
        _blogPosts.Add(blogPost);
        await SaveToFileAsync();
    }

    public async Task UpdateAsync(BlogPost blogPost)
    {
        var existingBlogPost = _blogPosts.FirstOrDefault(b => b.Id == blogPost.Id);
        if (existingBlogPost != null)
        {
            existingBlogPost.Username = blogPost.Username;
            existingBlogPost.DateCreated = blogPost.DateCreated;
            existingBlogPost.Text = blogPost.Text;
            await SaveToFileAsync();
        }
    }

    public async Task DeleteAsync(string encryptedId)
    {
        var blogPost = _blogPosts.FirstOrDefault(b => b.EncryptedId == encryptedId);
        if (blogPost != null)
        {
            _blogPosts.Remove(blogPost);
            await SaveToFileAsync();
        }
    }

    public async Task<int> GetIdByEncryptedIdAsync(string encryptedId)
    {
        var blogPost = _blogPosts.FirstOrDefault(b => b.EncryptedId == encryptedId);
        return await Task.FromResult(blogPost?.Id ?? 0);
    }

    public async Task<int> GetIdByEncryptedIdValAsync(string encryptedId)
    {
        var blogPost = _blogPosts.FirstOrDefault(b => b.EncryptedId == encryptedId);
        return await Task.FromResult(blogPost?.Id ?? 0);
    }
}
