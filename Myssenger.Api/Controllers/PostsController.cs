using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Myssenger.Api.Queries;

namespace Myssenger.Api.Controllers;

[Authorize]
[ApiController]
[Route("[Controller]")]
public sealed class PostsController
{
    private readonly IMediator _mediator;

    public PostsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> OneById([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new PostByIdQuery(id));

        return result.Match<IActionResult>(
            error => new NotFoundObjectResult(error.Value),
            success => new OkObjectResult(success.Value));
    }

    [HttpGet("popular")]
    public Task<IActionResult> MostPopular()
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public Task<IActionResult> Create()
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    public Task<IActionResult> Update()
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id:guid}")]
    public Task<IActionResult> Remove([FromRoute] Guid id)
    {
        throw new NotImplementedException();
    }
}