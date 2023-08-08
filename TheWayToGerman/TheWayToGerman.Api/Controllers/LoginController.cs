using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;
using TheWayToGerman.Core.Cqrs.Queries;
using TheWayToGerman.Core.Entities;
using TheWayToGerman.Core.Helpers;
using TheWayToGerman.Logic.Authentication;

namespace TheWayToGerman.Api.Controllers;
[Route("api/v1/login/")]
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
    [EnableRateLimiting("Auth")]
    public async Task<ActionResult> Authenticate(AuthenticationQuery command)
    {
        var result = await Mediator.Send(command);
        if (result.IsInternalError())
        {
            return Problem(result.GetErrorMessage());
        }
        if (result.ContainError())
        {
            return Problem(result.GetErrorMessage(), statusCode: StatusCodes.Status401Unauthorized);
        }
        return CreateToken(result.GetData());
    }
    ActionResult CreateToken(User user)
    {

        var result = AuthService.GenerateToken
                        (
                            (ClaimTypes.Role, user.UserType.ToString()),
                            (Constants.UserIDKey, user.Id.ToString()),
                            (Constants.UserNameKey, user.Name)
                        );

        if (result.IsInternalError())
        {
            return Problem(result.GetErrorMessage());
        }
        if (result.ContainError())
        {
            return Problem(result.GetErrorMessage(), statusCode: StatusCodes.Status401Unauthorized);
        }
        return Ok(new AuthToken { JwtToken = result.GetData() });



    }
}
