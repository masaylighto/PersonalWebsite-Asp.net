#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
using Bogus;
using System.Net.Http.Json;
using PersonalWebsiteApi.Core.Cqrs.Commands;
using Core.HTTP.Interfaces;
using IntegrationTest.Requests;
using Microsoft.AspNetCore.Http.HttpResults;
using IntegrationTest.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationTest.Endpoints;
public class OwnerTest
{
    readonly Faker Faker = new Faker();
    readonly IGenericHttpClient client;

    public OwnerTest()
    {
        client = WebApplicationBuilder.ApiClient();
    }


    [Fact]
    public async Task UpdateOwner_CorrectInformation_ShouldReturnOK()
    {
    }

  
    [Fact]
    public async Task UpdateOwner_PasswordLessThan8Char_ShouldReturnHttpBadRequest()
    {
      
    }
    [Fact]
    public async Task UpdateOwner_UsernameIsNull_ShouldReturnHttpBadRequest()
    {       
    }

    [Fact]
    public async Task UpdateOwner_EmailIsNull_ShouldReturnHttpBadRequest()
    {       
    }

    [Fact]
    public async Task UpdateOwner_NameIsNull_ShouldReturnHttpBadRequest()
    {       
    }

    [Fact]
    public async Task UpdateOwner_NoDataChanges_ShouldReturnHttpBadRequest()
    {
    }


    [Fact]
    public async Task AddOwner_UniqueValues_ShouldReturnHttpOK()
    {
        //prepare
        var generalTestRequest = new GeneralTestRequest<CreateFirstOwnerCommand, Ok>() {

            Endpoint = "api/v1/Owner",
            Content= new CreateFirstOwnerCommand()
            {
                Email = Faker.Internet.Email(),
                Name = Faker.Name.FullName(),
                Password = Faker.Internet.Password(8),
                Username = Faker.Internet.UserName(),
            }

        };
       //act
       var result = await client.PostAsync(generalTestRequest);

       //check
       result.FailTestIfError();
       var response = result.GetData(); 
       Assert.Equal(System.Net.HttpStatusCode.OK,response.StatusCode);
    }

    [Fact]
    public async Task AddOwner_OwnerAlreadyExist_ShouldReturnHttpBadRequest()
    {
        //prepare
        var generalTestRequest = new GeneralTestRequest<CreateFirstOwnerCommand, Ok>()
        {

            Endpoint = "api/v1/Owner",
            Content = new CreateFirstOwnerCommand()
            {
                Email = Faker.Internet.Email(),
                Name = Faker.Name.FullName(),
                Password = Faker.Internet.Password(8),
                Username = Faker.Internet.UserName(),
            }
        };
        await client.PostAsync(generalTestRequest);
        //act
        var result = await client.PostAsync(generalTestRequest);
        result.FailTestIfError();
        var response = result.GetData();
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);     
    }

    [Fact]
    public async Task AddOwner_PasswordLessThan8Char_ShouldReturnHttpBadRequest()
    {
       
    }

    [Fact]
    public async Task AddOwner_UsernameIsNull_ShouldReturnHttpBadRequest()
    {
     
    }

    [Fact]
    public async Task AddOwner_EmailIsNull_ShouldReturnHttpBadRequest()
    {
      
    }

    [Fact]
    public async Task AddOwner_NameIsNull_ShouldReturnHttpBadRequest()
    {
       
    }

}
