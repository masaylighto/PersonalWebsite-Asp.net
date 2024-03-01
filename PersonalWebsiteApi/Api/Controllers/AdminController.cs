
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsiteApi.Core.Cqrs.Commands.Admin;
using PersonalWebsiteApi.Core.Cqrs.Queries;
using PersonalWebsiteApi.Core.Exceptions;
using PersonalWebsiteApi.Core.Helpers;
using PersonalWebsiteApi.Core.ModelBinders.Models;

namespace PersonalWebsiteApi.Api.Controllers;

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
    public async Task<ActionResult> UpdateSelf([FromBody] UpdateAdminCommand command, UserAuthClaim UserAuthClaims)
    {
        command.Id = UserAuthClaims.ID;
        var result = await Mediator.Send(command);

        if (result.IsInternalError())
        {
            return Problem(result.GetErrorMessage());
        }
        if (result.ContainError())
        {
            return Problem(result.GetErrorMessage(), statusCode: StatusCodes.Status400BadRequest);
        }
        return Ok();
    }


    [HttpPost]
    [Authorize(AuthPolicies.OwnerPolicy)]
    public async Task<ActionResult> CreateAdmins([FromBody] CreateAdminCommand userCommand)
    {
        var result = await Mediator.Send(userCommand);
        if (result.IsInternalError())
        {
            return Problem(result.GetErrorMessage());
        }
        if (result.ContainError())
        {
            return Problem(result.GetErrorMessage(), statusCode: StatusCodes.Status400BadRequest);
        }
        return Ok(result.GetData());
    }
    [HttpGet]
    [Authorize(AuthPolicies.OwnerPolicy)]
    public async Task<ActionResult> GetAdmins([FromQuery]GetAdminsQuery userCommand)
    {
        var result = await Mediator.Send(userCommand);
        if (result.IsInternalError())
        {
            return Problem(result.GetErrorMessage());
        }
        if (result.ContainError())
        {
            return Problem(result.GetErrorMessage(), statusCode: StatusCodes.Status400BadRequest);
        }
        return Ok(result.GetData());
    }
    [HttpDelete]
    [Authorize(AuthPolicies.OwnerPolicy)]
    public async Task<ActionResult> DeleteAdmin([FromBody] DeleteAdminCommand userCommand)
    {
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
