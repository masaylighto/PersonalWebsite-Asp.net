using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheWayToGerman.Api.DTO;
using TheWayToGerman.Api.DTO.Owner;
using TheWayToGerman.Api.ResponseObject;
using TheWayToGerman.Api.ResponseObject.Owner;
using TheWayToGerman.Core.Cqrs.Commands;
using TheWayToGerman.Core.Cqrs.Queries;
using TheWayToGerman.Core.Enums;
using TheWayToGerman.Core.Exceptions;
using TheWayToGerman.Core.Helpers;

namespace TheWayToGerman.Api.Controllers;

[ApiController]
[Route("api/v1/Owner")]
public class OwnerController : ControllerBase
{
    public IMediator Mediator { get; }
    public OwnerController(IMediator mediator)
    {
        Mediator = mediator;
    }

    [HttpPost]
    [Route("Admin")]
    [Authorize(AuthPolicies.OwnerPolicy)]
    public async Task<ActionResult> CreateAdmins([FromBody] CreateAdminDTO DTO)
    {
        var userCommand = DTO.Adapt<CreateAdminCommand>();
        var result = await Mediator.Send(userCommand);
        if (result.ContainError())
        {
            return BadRequest(new ErrorResponse() { Error = result.GetError().Message });
        }
        return Ok();
    }
    [HttpPost]    
    [AllowAnonymous] // will only called once to create first Owner
    public async Task<ActionResult> CreateFirstOwner([FromBody] CreateFirstOwnerDTO DTO)
    {
        var userCommand = DTO.Adapt<CreateFirstOwnerCommand>();
        var result = await Mediator.Send(userCommand);
        if (result.ContainError())
        {
            return BadRequest(new ErrorResponse() { Error = result.GetError().Message });
        }
        return Ok();
    }
    [HttpGet]
    [Route("Admin")]
    [Authorize(AuthPolicies.OwnerPolicy)]
    public async Task<ActionResult> GetAdmins([FromQuery] GetAdminsDTO DTO)
    {
        var userCommand = DTO.Adapt<GetAdminsQuery>();
        var result = await Mediator.Send(userCommand);
        if (result.ContainError())
        {
            return BadRequest(new ErrorResponse() { Error = result.GetError().Message });
        }
        var Admins = result.GetData().Select(x => x.Adapt<GetAdminsResponse>());
        return Ok(Admins);
    }
    [HttpDelete]
    [Route("Admin")]
    [Authorize(AuthPolicies.OwnerPolicy)]
    public async Task<ActionResult> DeleteAdmin([FromBody] DeleteAdminDTO DTO)
    {
        var userCommand = DTO.Adapt<DeleteAdminCommand>();
        var result = await Mediator.Send(userCommand);
        if (result.IsErrorOfType<DataNotFoundException>())
        {
            return NotFound();
        }
        if (result.ContainError())
        {
            return Problem(result.GetError().Message);
        }
        return NoContent();
    }
    [HttpPut]
    [Authorize(AuthPolicies.OwnerPolicy)]
    public async Task<ActionResult> UpdateInformation([FromBody] UpdateUserInformationDTO DTO)
    {
        var userCommand = DTO.Adapt<UpdateOwnerInformationCommand>();
        var result = await Mediator.Send(userCommand);
        if (result.ContainError())
        {
            return BadRequest(new ErrorResponse() { Error = result.GetError().Message });
        }
        return Ok();
    }
}
