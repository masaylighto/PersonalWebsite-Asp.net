using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using PersonalWebsiteApi.Core.Cqrs.Queries;
using PersonalWebsiteApi.Logic.Authentication;

namespace IntegrationTest.Endpoints;

public class LoginTest
{
    [Fact]
    public async Task Authenticate_CorrectInfo_ShouldReturnJWTToken()
    {
      
    }


    [Fact]
    public async Task Authenticate_InCorrectInfo_ShouldReturnUnauthorized()
    {
      
    }
}
