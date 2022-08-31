using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeamMemberships.Service.Queries.GetAccountMemberships;
using TeamMemberships.Service.Queries.GetMembership;

namespace TeamMemberships.Api.Controllers
{
    [Route("membership")]
    [ApiController]
    public class TeamMembershipController : Controller
    {
        private readonly IMediator _mediator;

        public TeamMembershipController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        [HttpGet("get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] Guid teamGuid, [FromQuery] Guid accountGuid)
        {
            var query = new GetMembershipQuery(teamGuid, accountGuid);
            var queryResult = await _mediator.Send(query);

            return Ok(queryResult);
        }
        
        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] Guid accountGuid)
        {
            var query = new GetAccountMembershipsQuery(accountGuid);
            var queryResult = await _mediator.Send(query);
        
            return Ok(queryResult);
        }
        
        /*[HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid? teamGuid, Guid? accountGuid)
        {
            IBaseRequest query;

            if (teamGuid is null || accountGuid is null)
                query = new GetMembershipsQuery();
            else
                query = new GetMembershipQuery((Guid)teamGuid, (Guid)accountGuid);

            var queryResult = await _mediator.Send(query);

            return Ok(queryResult);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] MembershipCreateDto membershipCreateDto)
        {
            var command = new CreateMembershipCommand(membershipCreateDto);

            var commandResult = await _mediator.Send(command);

            return Ok(commandResult);
        }
        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
            [FromQuery] Guid teamGuid,
            [FromQuery] Guid userGuid,
            [FromBody] MembershipUpdateDto updateDto)
        {
            var command = new UpdateMembershipCommand(teamGuid, userGuid, updateDto);

            var commandResult = await _mediator.Send(command);

            return Ok(commandResult);
        }
        
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(
            [FromQuery] Guid teamGuid,
            [FromQuery] Guid userGuid)
        {
            var command = new DeleteMembershipCommand(teamGuid, userGuid);

            await _mediator.Send(command);

            return Ok();
        }*/
    }
}
