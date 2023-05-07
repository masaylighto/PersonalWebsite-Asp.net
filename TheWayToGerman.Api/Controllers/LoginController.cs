using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheWayToGerman.Api.DTO.Login;
using TheWayToGerman.Api.ResponseObject;
using TheWayToGerman.Core.Cqrs.Queries;
using TheWayToGerman.Logic.Authentication;

namespace TheWayToGerman.Api.Controllers;
[Route("v1/login/")]
[ApiController]
public class LoginController : ControllerBase
{
    public IMediator Mediator { get; }
    public IAuthService AuthService { get; }

    public LoginController(IMediator mediator, IAuthService authService)
    {
        Mediator = mediator;
        AuthService = authService;
    }

   
    [HttpPost]
    [Route("auth")]

    public async Task<ActionResult> Authenticate(AuthenticateDTO authenticateDTO)
    {
        var command = authenticateDTO.Adapt<GetUserToAuthQuery>();
        var commandresult = await Mediator.Send(command);
        if (commandresult.ContainError())
        {
            return Unauthorized(new ErrorResponse()
            {
                Error = commandresult.GetError().Message
            });
        }
        var user = commandresult.GetData();
        var tokenResult = AuthService.GenerateToken(user.UserType.ToString(), user.Id.ToString());
        if (tokenResult.ContainError())
        {
            return Unauthorized(new ErrorResponse()
            {
                Error = commandresult.GetError().Message
            });
        }
        return Ok(tokenResult.GetData());

    }
}
