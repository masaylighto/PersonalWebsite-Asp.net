

using Bogus;
using System.Net.Http.Json;
using TheWayToGerman.Api.DTO;
using TheWayToGerman.Api.DTO.Admin;
using TheWayToGerman.Api.DTO.Category;
using TheWayToGerman.Api.ResponseObject.Admin;
using TheWayToGerman.Core.Cqrs.Queries;
using TheWayToGerman.Core.Helpers;

namespace IntegrationTest.Endpoints;

public class CategoryTest
{
    readonly Faker Faker = new Faker();
    readonly HttpClient client;

    public CategoryTest()
    {
        client = WebApplicationBuilder.ApiClient();
    }

    [Fact]
    public async Task AddCategory_CorrectInformation_ShouldReturnOK()
    {
        //prepare
        await client.Authenticate();
        CreateCatagoryDTO createCatagoryDTO = new()
        {
            LanguageID =new Guid(DefaultDBValues.DefaultLanguageID),
            Name = Faker.Name.FullName()
        };
        //execute
        var result= await client.PostAsJsonAsync("api/v1/Category", createCatagoryDTO);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
    }




    [Fact]
    public async Task AddCategory_DuplicateName_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateCatagoryDTO createCatagoryDTO = new()
        {
            LanguageID = new Guid(DefaultDBValues.DefaultLanguageID),
            Name = Faker.Name.FullName()
        };
        CreateCatagoryDTO createCatagoryDTO2 = new()
        {
            LanguageID = new Guid(DefaultDBValues.DefaultLanguageID),
            Name = createCatagoryDTO.Name
        };
        //execute
        await client.PostAsJsonAsync("api/v1/Category", createCatagoryDTO);
        var result = await client.PostAsJsonAsync("api/v1/Category", createCatagoryDTO2);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task AddCategory_LanguageDoesntExist_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateCatagoryDTO createCatagoryDTO = new()
        {
            LanguageID = new Guid(),
            Name = Faker.Name.FullName()
        };
        //execute
        var result = await client.PostAsJsonAsync("api/v1/Category", createCatagoryDTO);       
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
}
