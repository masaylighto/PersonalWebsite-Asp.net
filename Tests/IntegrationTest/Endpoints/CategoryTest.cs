#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
using Bogus;
using System.Net.Http.Json;
using TheWayToGerman.Core.Cqrs.Commands;
using TheWayToGerman.Core.Enums;

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
        CreateCategoryCommand createCategoryDTO = new()
        {
            Language = Language.English,
            Name = Faker.Name.FullName()
        };
        //execute
        var result = await client.PostAsJsonAsync("api/v1/Category", createCategoryDTO);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
    }




    [Fact]
    public async Task AddCategory_DuplicateName_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateCategoryCommand createCategoryDTO = new()
        {
            Language = Language.English,
            Name = Faker.Name.FullName()
        };
        CreateCategoryCommand createCategoryDTO2 = new()
        {
            Language = Language.English,
            Name = createCategoryDTO.Name
        };
        //execute
        await client.PostAsJsonAsync("api/v1/Category", createCategoryDTO);
        var result = await client.PostAsJsonAsync("api/v1/Category", createCategoryDTO2);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }

}
