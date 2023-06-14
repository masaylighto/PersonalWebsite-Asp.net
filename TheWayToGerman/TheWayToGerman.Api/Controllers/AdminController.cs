using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TheWayToGerman.Api.DTO.Admin;
using TheWayToGerman.Api.ResponseObject;
using TheWayToGerman.Api.ResponseObject.Admin;
using TheWayToGerman.Core.Cqrs.Commands;
using TheWayToGerman.Core.Cqrs.Queries;
using TheWayToGerman.Core.Exceptions;
using TheWayToGerman.Core.Helpers;
using TheWayToGerman.Core.ModelBinders.Models;
using TheWayToGerman.Core.ParametersBinders;

namespace TheWayToGerman.Api.Controllers;

[ApiController]
[Route("api/v1/Admin")]
public class AdminController : ControllerBase
{
    public IMediator Mediator { get; }
    public AdminController(IMediator mediator)
    {
        Mediator = mediator;
    }

    [HttpPut]
    [Authorize(AuthPolicies.AdminPolicy)]
    public async Task<ActionResult> UpdateSelf([FromBody]UpdateAdminDTO DTO, UserAuthClaim UserAuthClaims)
    {
        var command = DTO.Adapt<UpdateAdminCommand>();
        command.Id = UserAuthClaims.ID;
        var result = await Mediator.Send(command);
        
        if (result.IsInternalError())
        {
            return Problem(result.GetErrorMessage());
        }
        if (result.ContainError())
        {
            return Problem(result.GetErrorMessage(),statusCode: StatusCodes.Status400BadRequest);
        }
        return Ok();
    }


    [HttpPost]
    [Authorize(AuthPolicies.OwnerPolicy)]
    public async Task<ActionResult> CreateAdmins([FromBody] CreateAdminDTO DTO)
    {
        var userCommand = DTO.Adapt<CreateAdminCommand>();
        var result = await Mediator.Send(userCommand);

        if (result.IsInternalError())
        {
            return Problem(result.GetErrorMessage());
        }
        if (result.ContainError())
        {
            return Problem(result.GetErrorMessage(), statusCode: StatusCodes.Status400BadRequest);
        }
        return Ok(result.GetData().Adapt<CreateAdminResponse>());
    }
    [HttpGet]
    [Authorize(AuthPolicies.OwnerPolicy)]
    public async Task<ActionResult> GetAdmins([FromQuery] GetAdminsDTO DTO)
    {
        var userCommand = DTO.Adapt<GetAdminsQuery>();
        var result = await Mediator.Send(userCommand);

        if (result.IsInternalError())
        {
            return Problem(result.GetErrorMessage());
        }
        if (result.ContainError())
        {
            return Problem(result.GetErrorMessage(), statusCode: StatusCodes.Status400BadRequest);
        }

        var Admins = result.GetData().Select(x => x.Adapt<GetAdminsResponse>());
        return Ok(Admins);
    }
    [HttpDelete]
    [Authorize(AuthPolicies.OwnerPolicy)]
    public async Task<ActionResult> DeleteAdmin([FromBody] DeleteAdminDTO DTO)
    {
        var userCommand = DTO.Adapt<DeleteAdminCommand>();
        var result = await Mediator.Send(userCommand);

        if (result.IsInternalError())
        {
            return Problem(result.GetErrorMessage());
        }
        if (result.IsErrorType<DataNotFoundException>())
        {
            return NotFound();
        }
        return NoContent();
    }
}
