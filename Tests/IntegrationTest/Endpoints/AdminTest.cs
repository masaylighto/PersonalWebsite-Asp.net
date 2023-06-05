

using Bogus;
using TheWayToGerman.Api.DTO;
using TheWayToGerman.Api.DTO.Admin;
using TheWayToGerman.Api.DTO.Owner;

namespace IntegrationTest.Endpoints;

public class AdminTest
{
    readonly Faker Faker = new Faker();
    readonly HttpClient client;

    public AdminTest()
    {
        client = WebApplicationBuilder.ApiClient();
    }

    [Fact]
    public async Task UpdateAdmin_CorrectInformation_ShouldReturnOK()
    {
        //prepare
        await client.Authenticate();
        CreateAdminDTO createAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };
        UpdateAdminDTO UpdateAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Person.FirstName,
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),
        };
        await client.PostAsJsonAsync("api/v1/Owner/Admin", createAdminDTO);
        await client.Authenticate(createAdminDTO.Username, createAdminDTO.Password);
        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(UpdateAdminDTO), HttpMethod.Put);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
    }




    [Fact]
    public async Task UpdateAdmin_DuplicateUserName_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
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
            Username = Faker.Internet.UserName(),

        };
        UpdateAdminDTO UpdateAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Person.FirstName,
            Password = Faker.Internet.Password(8),
            Username = createAdminDTO.Username,
        };
        await client.PostAsJsonAsync("api/v1/Owner/Admin", createAdminDTO);
        await client.PostAsJsonAsync("api/v1/Owner/Admin", createAdminDTO2);
        await client.Authenticate(createAdminDTO2.Username, createAdminDTO2.Password);

        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(UpdateAdminDTO), HttpMethod.Put);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task UpdateAdmin_DuplicateEmail_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
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
            Username = Faker.Internet.UserName(),

        };
        UpdateAdminDTO UpdateAdminDTO = new()
        {
            Email = createAdminDTO.Email,
            Name = Faker.Person.FirstName,
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),
        };
        await client.PostAsJsonAsync("api/v1/Owner/Admin", createAdminDTO);
        await client.PostAsJsonAsync("api/v1/Owner/Admin", createAdminDTO2);
        await client.Authenticate(createAdminDTO2.Username, createAdminDTO2.Password);

        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(UpdateAdminDTO), HttpMethod.Put);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task UpdateAdmin_PasswordLessThan8Char_ShouldReturnHttpBadRequest()
    {

        //prepare
        await client.Authenticate();
        CreateAdminDTO createAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),
        };
        UpdateAdminDTO UpdateAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Person.FirstName,
            Password = Faker.Internet.Password(7),
            Username = Faker.Internet.UserName(),
        };
        await client.PostAsJsonAsync("api/v1/Owner/Admin", createAdminDTO);
        await client.Authenticate(createAdminDTO.Username, createAdminDTO.Password);

        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(UpdateAdminDTO), HttpMethod.Put);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task UpdateAdmin_UsernameIsNull_ShouldReturnHttpBadRequest()
    {

        //prepare
        await client.Authenticate();
        CreateAdminDTO createAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),
        };
        UpdateAdminDTO UpdateAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Person.FirstName,
            Password = Faker.Internet.Password(7),
            Username = null,
        };
        await client.PostAsJsonAsync("api/v1/Owner/Admin", createAdminDTO);
        await client.Authenticate(createAdminDTO.Username, createAdminDTO.Password);

        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(UpdateAdminDTO), HttpMethod.Put);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task UpdateAdmin_EmailIsNull_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateAdminDTO createAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),
        };
        UpdateAdminDTO UpdateAdminDTO = new()
        {
            Email = null,
            Name = Faker.Person.FirstName,
            Password = Faker.Internet.Password(7),
            Username = Faker.Internet.UserName(),
        };
        await client.PostAsJsonAsync("api/v1/Owner/Admin", createAdminDTO);
        await client.Authenticate(createAdminDTO.Username, createAdminDTO.Password);

        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(UpdateAdminDTO), HttpMethod.Put);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task UpdateAdmin_NameIsNull_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateAdminDTO createAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),
        };
        UpdateAdminDTO UpdateAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = null,
            Password = Faker.Internet.Password(7),
            Username = Faker.Internet.UserName(),
        };
        await client.PostAsJsonAsync("api/v1/Owner/Admin", createAdminDTO);
        await client.Authenticate(createAdminDTO.Username, createAdminDTO.Password);

        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(UpdateAdminDTO), HttpMethod.Put);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task UpdateAdmin_NoDataChanges_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateAdminDTO createAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),
        };
        UpdateAdminDTO UpdateAdminDTO = new()
        {
            Email = createAdminDTO.Email,
            Name = createAdminDTO.Name,
            Password = createAdminDTO.Password,
            Username = createAdminDTO.Username,
        };
        await client.PostAsJsonAsync("api/v1/Owner/Admin", createAdminDTO);
        await client.Authenticate(createAdminDTO.Username, createAdminDTO.Password);

        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(UpdateAdminDTO), HttpMethod.Put);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }

}
