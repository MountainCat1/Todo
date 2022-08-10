using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamMemberships.Service.Abstractions;
using TeamMemberships.Service.Commands.CreateMembership;
using TeamMemberships.Service.Commands.DeleteMembership;
using TeamMemberships.Service.Commands.UpdateMembership;
using TeamMemberships.Service.Dto;
using TeamMemberships.Service.Queries.GetMembership;
using TeamMemberships.Service.Queries.GetMemberships;

namespace TeamMemberships.Api.Controllers
{
    [Route("api/teamMembership")]
    [ApiController]
    public class TeamMembershipController : Controller
    {
        private readonly IMediator _mediator;

        public TeamMembershipController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid? teamGuid, Guid? userGuid)
        {
            IBaseRequest query;

            if (teamGuid is null || userGuid is null)
                query = new GetMembershipsQuery();
            else
                query = new GetMembershipQuery((Guid)teamGuid, (Guid)userGuid);

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
        }
    }
}
