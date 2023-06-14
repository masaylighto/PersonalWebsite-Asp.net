using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TheWayToGerman.Api.DTO;
using TheWayToGerman.Api.DTO.Owner;
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
    [AllowAnonymous] // will only called once to create first Owner
    public async Task<ActionResult> CreateFirstOwner([FromBody] CreateFirstOwnerDTO DTO)
    {
        var userCommand = DTO.Adapt<CreateFirstOwnerCommand>();
        var result = await Mediator.Send(userCommand);


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
   
    [HttpPut]
    [Authorize(AuthPolicies.OwnerPolicy)]
    public async Task<ActionResult> UpdateInformation([FromBody] UpdateUserInformationDTO DTO)
    {
        var userCommand = DTO.Adapt<UpdateOwnerInformationCommand>();
        var result = await Mediator.Send(userCommand);

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
}
