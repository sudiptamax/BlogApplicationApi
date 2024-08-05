using BlogApi.Models;

namespace BlogApi.Handlers
{
    public class GetBlogPostsQueryResult
    {
        public IEnumerable<BlogPost> BlogPosts { get; set; }
        public int TotalRecords { get; set; }
    }

    public class PaginatedResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int totalRecords { get; set; }
    }
}
