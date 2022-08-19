using System.Security.Claims;
using Authentication.Service.Commands.RegisterAccount;
using Authentication.Service.Dto;
using Authentication.Service.Queries.GetAccountJwt;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Api.Controllers;

[ApiController]
[Route("Authentication")]
public class AuthenticationController : Controller
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAccount([FromBody] AccountRegisterDto registerDto)
    {
        var command = new RegisterAccountCommand(registerDto);

        var commandResult = await _mediator.Send(command);

        return Ok(commandResult);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] AccountLoginDto loginDto)
    {
        var query = new GetAccountJwtQuery(loginDto);

        var queryResult = await _mediator.Send(query);

        return Ok(queryResult);
    }

    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("claims")]
    public IActionResult GetClaims()
    {
        var claims = User.Claims.ToList();
        
        return Ok(claims.ToDictionary(claim =>claim.Type, claim => claim.Value));
    }
}