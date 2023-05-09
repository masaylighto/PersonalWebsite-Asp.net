using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TheWayToGerman.Api.DTO.Owner;
using TheWayToGerman.Api.ResponseObject;
using TheWayToGerman.Core.Attributes;
using TheWayToGerman.Core.Cqrs.Commands;
using TheWayToGerman.Core.Enums;

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
    [AccessibleBy(UserType.Owner)]
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
}
