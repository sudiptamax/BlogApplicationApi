using BlogApi.Handlers;
using BlogApi.Models;
using MediatR;
using System.Collections.Generic;

namespace BlogApi.Queries
{
    public class GetBlogPostsQuery : IRequest<PaginatedResult<BlogPostResponse>>
    {
        public string Search { get; set; }
        public string SortBy { get; set; }
        public string SortDirection { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }

    

}



