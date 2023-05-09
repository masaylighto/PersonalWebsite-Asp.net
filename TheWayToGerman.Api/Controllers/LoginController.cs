using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Reflection.Metadata;
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
        var commandresult = await Mediator.Send(command);
        if (commandresult.ContainError())
        {
            return Unauthorized(new ErrorResponse()
            {
                Error = commandresult.GetError().Message
            });
        }
        var user = commandresult.GetData();
        var tokenResult = AuthService.GenerateToken
                        (
                            (Constants.UserTypeKey , user.UserType.ToString()),
                            (Constants.UserIDKey , user.Id.ToString())
                        );
        if (tokenResult.ContainError())
        {
            return Unauthorized(new ErrorResponse()
            {
                Error = commandresult.GetError().Message
            });
        }
        return Ok(new AuthenticateResponse() { JwtToken = tokenResult.GetData() });

    }
}
