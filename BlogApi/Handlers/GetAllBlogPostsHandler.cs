using BlogApi.Models;
using BlogApi.Queries;
using BlogApi.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlogApi.Handlers
{
    public class GetBlogPostsHandler : IRequestHandler<GetBlogPostsQuery, PaginatedResult<BlogPostResponse>>
    {
        private readonly IBlogRepository _repository;

        public GetBlogPostsHandler(IBlogRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<BlogPostResponse>> Handle(GetBlogPostsQuery request, CancellationToken cancellationToken)
        {
            var blogPosts = await _repository.GetAllAsync();

            // Search
            if (!string.IsNullOrEmpty(request.Search))
            {
                blogPosts = blogPosts.Where(bp => bp.Username.Contains(request.Search) || bp.Text.Contains(request.Search));
            }

            // Sort
            if (!string.IsNullOrEmpty(request.SortBy))
            {
                blogPosts = request.SortBy.ToLower() switch
                {
                    "username" when request.SortDirection?.ToLower() == "desc" => blogPosts.OrderByDescending(bp => bp.Username),
                    "username" => blogPosts.OrderBy(bp => bp.Username),
                    "datecreated" when request.SortDirection?.ToLower() == "desc" => blogPosts.OrderByDescending(bp => bp.DateCreated),
                    "datecreated" => blogPosts.OrderBy(bp => bp.DateCreated),
                    _ => blogPosts
                };
            }

            var totalRecords = blogPosts.Count();

            // Pagination
            if (request.Page.HasValue && request.PageSize.HasValue)
            {
                blogPosts = blogPosts.Skip((request.Page.Value - 1) * request.PageSize.Value).Take(request.PageSize.Value);
            }

            return new PaginatedResult<BlogPostResponse>
            {
                Items = blogPosts.ToList(),
                totalRecords = totalRecords
            };
        }
    }




}
