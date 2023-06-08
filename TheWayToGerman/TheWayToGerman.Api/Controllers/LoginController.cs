using Core.DataKit.Result;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;
using TheWayToGerman.Api.DTO.Login;
using TheWayToGerman.Api.ResponseObject;
using TheWayToGerman.Api.ResponseObject.Login;
using TheWayToGerman.Core.Cqrs.Queries;
using TheWayToGerman.Core.Entities;
using TheWayToGerman.Core.Exceptions;
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
    public async Task<ActionResult> Authenticate(AuthenticateDTO authenticateDTO)
    {
        var command = authenticateDTO.Adapt<GetUserToAuthQuery>();
        var result = await Mediator.Send(command);

        if (result.IsInternalError())
        {
            return Problem(result.GetErrorMessage());
        }
        if (result.ContainError())
        {
            return Unauthorized(new ErrorResponse()
            {
                Error = result.GetErrorMessage()
            });
        }
        return  CreateToken(result.GetData());
    }
    ActionResult CreateToken(User user)
    {
       
        var tokenResult = AuthService.GenerateToken
                        (
                            (ClaimTypes.Role, user.UserType.ToString()),
                            (Constants.UserIDKey, user.Id.ToString())
                        );


        if (tokenResult.IsInternalError())
        {
            return Problem(tokenResult.GetErrorMessage());
        }

        if (tokenResult.ContainError())
        {
            return Unauthorized(new ErrorResponse()
            {
                Error = tokenResult.GetErrorMessage()
            });
        }

        return Ok(new AuthenticateResponse() { JwtToken = tokenResult.GetData() });



    }
}
