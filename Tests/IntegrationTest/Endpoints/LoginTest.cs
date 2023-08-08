using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using TheWayToGerman.Core.Cqrs.Queries;
using TheWayToGerman.Logic.Authentication;

namespace IntegrationTest.Endpoints;

public class LoginTest
{
    [Fact]
    public async Task Authenticate_CorrectInfo_ShouldReturnJWTToken()
    {
        //prepare
        var dto = new AuthenticationQuery() { Username = "masaylighto", Password = "8PS33gotf24a" };
        var client = WebApplicationBuilder.ApiClient();
        await client.AddFirstOwner(dto.Username, dto.Password);
        //execute
        var result = await client.PostAsJsonAsync("api/v1/login/auth", dto);
        //validate  
        Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        var authenticateResponse = await result.Content.ReadFromJsonAsync<AuthToken>();
        Assert.NotNull(authenticateResponse?.JwtToken);
    }


    [Fact]
    public async Task Authenticate_InCorrectInfo_ShouldReturnUnauthorized()
    {
        //prepare
        var dto = new AuthenticationQuery() { Username = "ad", Password = "ads" };
        var client = WebApplicationBuilder.ApiClient();
        //execute
        var result = await client.PostAsJsonAsync("api/v1/login/auth", dto);
        //validate  
        Assert.Equal(System.Net.HttpStatusCode.Unauthorized, result.StatusCode);
        var errorResponse = await result.Content.ReadFromJsonAsync<ProblemDetails>();
        Assert.NotNull(errorResponse?.Detail);
    }
}
