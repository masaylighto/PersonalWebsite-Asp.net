using Core.DataKit.Result;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Primitives;
using TheWayToGerman.Core.Exceptions;
using TheWayToGerman.Core.Helpers;
using TheWayToGerman.Logic.Authentication;

namespace TheWayToGerman.Api.Middleware;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _Next;

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _Next = next;
    }
    public async Task InvokeAsync(HttpContext httpContext, IAuthService authService)
    {
        //Get the Authentication key from post header ,
        //Parse it and store the claim into the route
        var JwtToken = GetAuthToken(httpContext);
        if (JwtToken.ContainError() || string.IsNullOrEmpty(JwtToken.GetData()))
        {
            await _Next(httpContext);
            return;
        }
        var decryptedTokenResult = authService.DecryptToken(JwtToken.GetData());            
        if (decryptedTokenResult.ContainError())
        {
            await _Next(httpContext);
            return;
        }
        var decryptedToken = decryptedTokenResult.GetData();
        if (!decryptedToken.IsValid)
        {
            await _Next(httpContext);
            return;
        }
        if (decryptedToken.Claims is null)
        {
            await _Next(httpContext);
            return;
        }
        var userType = decryptedToken.Claims.FirstOrDefault(c => c.Type == Constants.UserTypeKey);
        var userID   = decryptedToken.Claims.FirstOrDefault(c => c.Type == Constants.UserIDKey);
        if (userID is null || userType is null)
        {
            await _Next(httpContext);
            return;
        }
        httpContext.Request.RouteValues.Add(Constants.UserTypeKey, userType.Value);
        httpContext.Request.RouteValues.Add(Constants.UserIDKey, userID.Value);       
        await _Next(httpContext);
    }

    public Result<string> GetAuthToken(HttpContext httpContext)
    {
        StringValues accessToken;
        if (httpContext.Request.Headers.TryGetValue("Authorization", out accessToken))
        {
            return accessToken.ToString().Replace("Bearer ", "");
        }
        return new NullValueException("accessToken is null");
       
    }
}
public static class AuthenticationMiddlewareExtension
{
    public static void AddAuthenticationMiddleware(this WebApplication webApplication)
    {

        webApplication.UseMiddleware<AuthenticationMiddleware>();
    }
}