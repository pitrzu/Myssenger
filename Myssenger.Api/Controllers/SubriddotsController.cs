using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Myssenger.Api.Dtos;
using Myssenger.Api.Queries;
using Myssenger.Api.Queries.Subriddots;

namespace Myssenger.Api.Controllers;

[Authorize]
[ApiController]
[Route("[Controller]")]
public sealed class SubriddotsController
{
    private readonly IMediator _mediator;

    public SubriddotsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("id/{subriddotId:guid}")]
    [ProducesResponseType(type: typeof(GetSubriddotDto), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOneById([FromRoute] Guid subriddotId)
    {
        var query = new GetOneByIdQuery(subriddotId);
        var result = await _mediator.Send(query);

        return result.Match<IActionResult>(
            error => new NotFoundObjectResult(error.Value),
            success => new OkObjectResult(success.Value)
        );
    }

    [HttpGet("name/{name}")]
    [ProducesResponseType(type: typeof(IEnumerable<GetSubriddotDto>), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllMatchingName([FromRoute] string name)
    {
        var query = new GetAllByNameQuery(name);
        var result = await _mediator.Send(query);

        return result.Count() switch
        {
            0 => new NotFoundResult(),
            _ => new OkObjectResult(result)
        };
    }

    [HttpPost]
    [ProducesResponseType(statusCode: StatusCodes.Status201Created)]
    [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSubriddot([FromBody] CreateSubriddotDto dto)
    {
        /* TODO Should add authentication
         * TODO Should add authorization
         * TODO Should map the dto to the entity
         * TODO Should persist the entity to db and return its id as its route*/
        throw new NotImplementedException();
    }

    [HttpPut("{subriddotId:guid}")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(statusCode: StatusCodes.Status201Created)]
    [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateSubriddot([FromRoute] Guid subriddotId, [FromBody] UpdateSubriddotDto dto)
    {
        /* TODO Should add authentication
         * TODO Should add authorization
         * TODO Should map the dto to the entity
         * TODO Should update or persist the entity to the db*/
        throw new NotImplementedException();
    }

    [HttpDelete("{subriddotId:guid}")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveSubriddot([FromRoute] Guid subriddotId)
    {
        /* TODO Should add authentication
         * TODO Should add authorization
         * TODO Should remove the entity from DB*/
        throw new NotImplementedException();
    }

    [HttpPost("/promote/{userId:guid}")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PromoteModerator([FromBody] Guid userId)
    {
        /* TODO Should add authentication
         * TODO Should add authorization
         * TODO Should promote user to moderator*/
        throw new NotImplementedException();
    }

    [HttpPost("/demote/{userId:guid}")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DemoteFromModerator([FromBody] Guid userId)
    {
        /* TODO Should add authentication
         * TODO Should add authorization
         * TODO Should demote user to moderator*/
        throw new NotImplementedException();
    }

    [HttpGet("{subriddotId:guid}/posts")]
    [ProducesResponseType(type: typeof(IEnumerable<GetPostDto>), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPosts([FromBody] Guid subriddotId)
    {
        /* TODO Should add authentication
         * TODO Should add authorization
         * TODO Should retrieve subriddot posts from database */
        throw new NotImplementedException();
    }
}