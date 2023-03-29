using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Mysennger.Domain.Subriddots.ValueObjects;

namespace Myssenger.Api.Controllers;

[Route("[Controller]")]
[ApiController]
public class SubriddotsController
{
    public IActionResult GetById(SubriddotId id)
    {
        return new OkObjectResult(new { });
    }

    public IActionResult GetByNameLike(string name)
    {
        return new OkObjectResult(new { });
    }
    
    public IActionResult GetAll()
    {
        return new OkObjectResult(new { });
    }
    
    public IActionResult Create()
    {
        return new CreatedResult("hashjsh", new {});
    }

    public IActionResult Update()
    {
        return new OkResult();
    }

    public IActionResult Remove(SubriddotId subriddotId)
    {
        return new OkResult();
    }
}