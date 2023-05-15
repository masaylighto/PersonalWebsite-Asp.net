using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TheWayToGerman.Api.DTO;
using TheWayToGerman.Api.DTO.Owner;
using TheWayToGerman.Api.ResponseObject;
using TheWayToGerman.Core.Cqrs.Commands;
using TheWayToGerman.Core.Enums;
using TheWayToGerman.Core.Helpers;

namespace TheWayToGerman.Api.Controllers;

[ApiController]
[Route("v1/Owner")]
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
    public async Task<ActionResult> CreateAdmins([FromBody]CreateAdminDTO DTO)
    {
       var userCommand = DTO.Adapt<CreateUserCommand>();
       userCommand.UserType = UserType.Admin;
       var result =await Mediator.Send(userCommand);       
       if (result.ContainError())
       {
            return BadRequest(new ErrorResponse() { Error = result.GetError().Message });
       }
       return Ok();
    }

    [HttpPut]
    [Route("Self")]
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
