using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Service.Commands.CreateUser;
using Users.Service.Commands.DeleteUser;
using Users.Service.Commands.UpdateUser;
using Users.Service.Dto;
using Users.Service.Query.GetUser;
using Users.Service.Query.GetUsers;

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

    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromQuery] Guid? guid)
    {
        IBaseRequest query = guid is null
            ? new GetUsersQuery()
            : new GetUserQuery((Guid)guid);

        var queryResult = await _mediator.Send(query);

        return Ok(queryResult);
    }
    
    [HttpPost("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] UserCreateDto createDto)
    {
        var command = new CreateUserCommand(createDto);
        
        var commandResult = await _mediator.Send(command);

        return Ok(commandResult);
    }

    [HttpPut("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromQuery] Guid guid, [FromBody] UserUpdateDto updateDto)
    {
        var command = new UpdateUserCommand(guid, updateDto);
        
        var commandResult = await _mediator.Send(command);

        return Ok(commandResult);
    }
    
    [HttpDelete("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromQuery] Guid guid)
    {
        var command = new DeleteUserCommand(guid);
        
        await _mediator.Send(command);

        return Ok();
    }
}