

using Bogus;
using Humanizer;
using System.Net.Http.Json;
using TheWayToGerman.Api.DTO.Login;
using TheWayToGerman.Api.DTO.Owner;
using TheWayToGerman.Api.ResponseObject.Login;

namespace IntegrationTest.Features;

public class OwnerTest
{
    readonly Faker Faker = new Faker();
    readonly HttpClient client = WebApplicationBuilder.ApiClient();
        
    async Task Auth()
    {
        var dto = new AuthenticateDTO() { Username = "masaylighto", Password = "8PS33gotf24a" };      
        //execute
        var result = await client.PostAsJsonAsync("v1/login/auth", dto);
        var response =await result.Content.ReadFromJsonAsync<AuthenticateResponse>();
        client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {response.JwtToken}");

    }
    [Fact]
    public async Task AddAdmin_UniqueValues_ShouldReturnHttpOK()
    {
        //prepare
        await Auth();
        CreateAdminDTO createAdminDTO = new()
        { 
            Email = Faker.Internet.Email(),
            Name= Faker.Name.FullName(),
            Password= Faker.Internet.Password(8),
            Username= Faker.Internet.UserName(),
            
        };

        //execute
        var result = await client.PostAsJsonAsync("v1/Owner/Admin", createAdminDTO);
        
        //validate
        Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
    }


    [Fact]
    public async Task AddAdmin_DublicateUserName_ShouldReturnHttpBadRequest()
    {
        //prepare
        await Auth();
        CreateAdminDTO createAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };
        CreateAdminDTO createAdminDTO2 = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = createAdminDTO.Username,

        };
        
        //execute
         await client.PostAsJsonAsync("v1/Owner/Admin", createAdminDTO);
        
        var result = await client.PostAsJsonAsync("v1/Owner/Admin", createAdminDTO2);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task AddAdmin_DublicateEmail_ShouldReturnHttpBadRequest()
    {
        //prepare
        await Auth();
        CreateAdminDTO createAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };
        CreateAdminDTO createAdminDTO2 = new()
        {
            Email = createAdminDTO.Email,
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };

        //execute
        await client.PostAsJsonAsync("v1/Owner/Admin", createAdminDTO);

        var result = await client.PostAsJsonAsync("v1/Owner/Admin", createAdminDTO2);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task AddAdmin_PasswordLessThan8Char_ShouldReturnHttpBadRequest()
    {
        //prepare
        await Auth();
        CreateAdminDTO createAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(7),
            Username = Faker.Internet.UserName(),

        };

        //execute
        var result = await client.PostAsJsonAsync("v1/Owner/Admin", createAdminDTO);

        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task AddAdmin_UsernameIsNull_ShouldReturnHttpBadRequest()
    {
        //prepare
        await Auth();
        CreateAdminDTO createAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(7),
            Username = null,

        };

        //execute
        var result = await client.PostAsJsonAsync("v1/Owner/Admin", createAdminDTO);

        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task AddAdmin_EmailIsNull_ShouldReturnHttpBadRequest()
    {
        //prepare
        await Auth();
        CreateAdminDTO createAdminDTO = new()
        {
            Email = null,
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(7),
            Username = Faker.Internet.UserName(),

        };

        //execute
        var result = await client.PostAsJsonAsync("v1/Owner/Admin", createAdminDTO);

        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task AddAdmin_NameIsNull_ShouldReturnHttpBadRequest()
    {
        //prepare
        await Auth();
        CreateAdminDTO createAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = null,
            Password = Faker.Internet.Password(7),
            Username = Faker.Internet.UserName(),

        };

        //execute
        var result = await client.PostAsJsonAsync("v1/Owner/Admin", createAdminDTO);

        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
}
