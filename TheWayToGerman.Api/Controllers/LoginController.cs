using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Reflection.Metadata;
using System.Security.Claims;
using TheWayToGerman.Api.DTO.Login;
using TheWayToGerman.Api.ResponseObject;
using TheWayToGerman.Api.ResponseObject.Login;
using TheWayToGerman.Core.Cqrs.Queries;
using TheWayToGerman.Core.Helpers;
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
    [Route("Auth")]

    public async Task<ActionResult> Authenticate(AuthenticateDTO authenticateDTO)
    {
        var command = authenticateDTO.Adapt<GetUserToAuthQuery>();
        var commandResult = await Mediator.Send(command);
        if (commandResult.ContainError())
        {
            return Unauthorized(new ErrorResponse()
            {
                Error = commandResult.GetError().Message
            });
        }
        var user = commandResult.GetData();
        var tokenResult = AuthService.GenerateToken
                        (
                            (ClaimTypes.Role     , user.UserType.ToString()),
                            (Constants.UserIDKey , user.Id.ToString())
                        );
        if (tokenResult.ContainError())
        {
            return Unauthorized(new ErrorResponse()
            {
                Error = commandResult.GetError().Message
            });
        }
        return Ok(new AuthenticateResponse() { JwtToken = tokenResult.GetData() });

    }
}
