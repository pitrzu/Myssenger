using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Myssenger.Api.Dtos;
using Myssenger.Api.Queries.Posts;

namespace Myssenger.Api.Controllers;

[Authorize]
[ApiController]
[Route("[Controller]")]
public sealed class PostsController
{
    private readonly IMediator _mediator;
    private readonly IUserProvider _userProvider;
    
    public PostsController(IMediator mediator, IUserProvider userProvider)
    {
        _mediator = mediator;
        _userProvider = userProvider;
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(PostDto))]
    [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
    [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> OneById([FromRoute] Guid id)
    {
        var query = new PostByIdQuery(id);

        var result = await _mediator.Send(query);

        return result.Match<IActionResult>(
            cException => new BadRequestObjectResult(cException),
            aException => new NotFoundObjectResult(aException),
            post => new OkObjectResult(new PostDto(post)));
    }
    
    [HttpGet("popular")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ICollection<PostDto>))]
    [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
    [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> MostPopular([FromQuery] string filter, [FromQuery] string sort, [FromQuery] int? page, [FromQuery] int? perPage)
    {
        var query = new MostPopularPostsQuery(filter, sort, page, perPage);

        var result = await _mediator.Send(query);

        return result.Match<IActionResult>(
            aException => new BadRequestObjectResult(aException),
            exception => new NotFoundObjectResult(exception),
            list => new OkObjectResult(list.Select(post => new PostDto(post))));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PostDto dto)
    {
        throw new NotImplementedException();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] PostDto dto)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Remove([FromRoute] Guid id)
    {
        throw new NotImplementedException();
    }
}