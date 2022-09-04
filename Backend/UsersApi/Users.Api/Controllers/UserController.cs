using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Service.Query.GetUser;
using Users.Service.Query.GetUserFromClaims;

namespace Users.Api.Controllers;


[ApiController]
[Route("user")]
public class UserController : Controller
{
    private readonly IMediator _mediator;
    
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("get")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromQuery] Guid guid)
    {
        var query = new GetUserQuery(guid);

        var queryResult = await _mediator.Send(query);

        return Ok(queryResult);
    }
    
    [Authorize]
    [HttpGet("me")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMyUserData()
    {
        var query = new GetUserFromClaimsQuery(User);

        var queryResult = await _mediator.Send(query);

        return Ok(queryResult);
    }
}