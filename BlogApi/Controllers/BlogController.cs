using BlogApi.Commands;
using BlogApi.Handlers;
using BlogApi.Models;
using BlogApi.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BlogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new { id = result.EncryptedId, result.Username, result.DateCreated, result.Text });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlogPost(string id, [FromBody] UpdateBlogPostCommand command)
        {
            command.id = id;
            var result = await _mediator.Send(command);
            return result == null ? NotFound() : Ok(new { id = result.EncryptedId, result.Username, result.DateCreated, result.Text });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogPost(string id)
        {
            var command = new DeleteBlogPostCommand { EncryptedId = id };
            await _mediator.Send(command);
            return NoContent();
        }

        //[HttpGet]
        //public async Task<IActionResult> GetBlogPosts([FromQuery] string? search, [FromQuery] string? sortBy, [FromQuery] string? sortDirection, [FromQuery] int? page, [FromQuery] int? pageSize)
        //{
        //    var query = new GetBlogPostsQuery
        //    {
        //        Search = search,
        //        SortBy = sortBy,
        //        SortDirection = sortDirection,
        //        Page = page,
        //        PageSize = pageSize
        //    };

        //    //var result = await _mediator.Send(query);
        //    //return Ok(result.Select(bp => new { id = bp.EncryptedId, bp.Username, bp.DateCreated, bp.Text }));

        //    var result = await _mediator.Send(query);
        //    return Ok(result);
        //}

        [HttpGet]
        public async Task<IActionResult> GetBlogs([FromQuery] string search = "", [FromQuery] string sortBy = "username", [FromQuery] string sortDirection = "asc", [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetBlogPostsQuery
            {
                Search = search,
                SortBy = sortBy,
                SortDirection = sortDirection,
                Page = page,
                PageSize = pageSize
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogPostById(string id)
        {
            var query = new GetBlogPostByEncryptedIdQuery { EncryptedId = id };
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(new { result.Id, result.Username, result.DateCreated, result.Text });
        }
    }
}
