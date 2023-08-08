#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
using Bogus;
using System.Net.Http.Json;
using TheWayToGerman.Core.Cqrs.Commands.Admin;
using TheWayToGerman.Core.Cqrs.Queries;

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
        CreateAdminCommand createAdminCommand = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };
        UpdateAdminCommand UpdateAdminCommand = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Person.FirstName,
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),
        };
        await client.PostAsJsonAsync("api/v1/Admin", createAdminCommand);
        await client.Authenticate(createAdminCommand.Username, createAdminCommand.Password);
        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(UpdateAdminCommand), HttpMethod.Put);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
    }




    [Fact]
    public async Task UpdateAdmin_DuplicateUserName_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateAdminCommand createAdminCommand = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),
        };
        CreateAdminCommand createAdminCommand2 = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };
        UpdateAdminCommand UpdateAdminCommand = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Person.FirstName,
            Password = Faker.Internet.Password(8),
            Username = createAdminCommand.Username,
        };
        await client.PostAsJsonAsync("api/v1/Admin", createAdminCommand);
        await client.PostAsJsonAsync("api/v1/Admin", createAdminCommand2);
        await client.Authenticate(createAdminCommand2.Username, createAdminCommand2.Password);

        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(UpdateAdminCommand), HttpMethod.Put);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task UpdateAdmin_DuplicateEmail_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateAdminCommand createAdminCommand = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),
        };
        CreateAdminCommand createAdminCommand2 = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };
        UpdateAdminCommand UpdateAdminCommand = new()
        {
            Email = createAdminCommand.Email,
            Name = Faker.Person.FirstName,
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),
        };
        await client.PostAsJsonAsync("api/v1/Admin", createAdminCommand);
        await client.PostAsJsonAsync("api/v1/Admin", createAdminCommand2);
        await client.Authenticate(createAdminCommand2.Username, createAdminCommand2.Password);

        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(UpdateAdminCommand), HttpMethod.Put);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task UpdateAdmin_PasswordLessThan8Char_ShouldReturnHttpBadRequest()
    {

        //prepare
        await client.Authenticate();
        CreateAdminCommand createAdminCommand = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),
        };
        UpdateAdminCommand UpdateAdminCommand = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Person.FirstName,
            Password = Faker.Internet.Password(7),
            Username = Faker.Internet.UserName(),
        };
        await client.PostAsJsonAsync("api/v1/Admin", createAdminCommand);
        await client.Authenticate(createAdminCommand.Username, createAdminCommand.Password);

        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(UpdateAdminCommand), HttpMethod.Put);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task UpdateAdmin_UsernameIsNull_ShouldReturnHttpBadRequest()
    {

        //prepare
        await client.Authenticate();
        CreateAdminCommand createAdminCommand = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),
        };
        UpdateAdminCommand UpdateAdminCommand = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Person.FirstName,
            Password = Faker.Internet.Password(7),
            Username = null,
        };
        await client.PostAsJsonAsync("api/v1/Admin", createAdminCommand);
        await client.Authenticate(createAdminCommand.Username, createAdminCommand.Password);

        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(UpdateAdminCommand), HttpMethod.Put);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task UpdateAdmin_EmailIsNull_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateAdminCommand createAdminCommand = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),
        };
        UpdateAdminCommand UpdateAdminCommand = new()
        {
            Email = null,
            Name = Faker.Person.FirstName,
            Password = Faker.Internet.Password(7),
            Username = Faker.Internet.UserName(),
        };
        await client.PostAsJsonAsync("api/v1/Admin", createAdminCommand);
        await client.Authenticate(createAdminCommand.Username, createAdminCommand.Password);

        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(UpdateAdminCommand), HttpMethod.Put);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task UpdateAdmin_NameIsNull_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateAdminCommand createAdminCommand = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),
        };
        UpdateAdminCommand UpdateAdminCommand = new()
        {
            Email = Faker.Internet.Email(),
            Name = null,
            Password = Faker.Internet.Password(7),
            Username = Faker.Internet.UserName(),
        };
        await client.PostAsJsonAsync("api/v1/Admin", createAdminCommand);
        await client.Authenticate(createAdminCommand.Username, createAdminCommand.Password);

        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(UpdateAdminCommand), HttpMethod.Put);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task UpdateAdmin_NoDataChanges_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateAdminCommand createAdminCommand = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),
        };
        UpdateAdminCommand UpdateAdminCommand = new()
        {
            Email = createAdminCommand.Email,
            Name = createAdminCommand.Name,
            Password = createAdminCommand.Password,
            Username = createAdminCommand.Username,
        };
        await client.PostAsJsonAsync("api/v1/Admin", createAdminCommand);
        await client.Authenticate(createAdminCommand.Username, createAdminCommand.Password);

        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(UpdateAdminCommand), HttpMethod.Put);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Fact]
    public async Task GetAdmins_NonExistingName_ShouldReturnHttpOkWithEmptyArray()
    {
        //prepare
        await client.Authenticate();
        CreateAdminCommand createAdminCommand = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(7),
            Username = Faker.Internet.UserName(),

        };
        GetAdminsQuery GetAdminCommand = new()
        {
            Name = "SomeRandomName",
        };
        await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(createAdminCommand), HttpMethod.Post);
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(GetAdminCommand), HttpMethod.Get);
        //execute
        //validate
        Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        var userlist = await result.Content.ReadFromJsonAsync<IEnumerable<GetAdminsQueryResponse>>();
        Assert.NotNull(userlist);
        Assert.Empty(userlist);
    }
    [Fact]
    public async Task GetAdmins_ExistingName_ShouldReturnHttpOkWithArrayOfMatchedUser()
    {
        //prepare
        await client.Authenticate();
        CreateAdminCommand createAdminCommand = new()
        {
            Email = Faker.Internet.Email(),
            Name = "SomeSpecificName",
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };
        GetAdminsQuery GetAdminCommand = new()
        {
            Name = "SomeSpecificName",
        };
        await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(createAdminCommand), HttpMethod.Post);
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(GetAdminCommand), HttpMethod.Get);
        //execute
        //validate
        Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        var userlist = await result.Content.ReadFromJsonAsync<IEnumerable<GetAdminsQueryResponse>>();
        Assert.NotNull(userlist);
        Assert.Contains(userlist, x => x.Email == createAdminCommand.Email);
        Assert.Contains(userlist, x => x.Name == createAdminCommand.Name);
        Assert.Contains(userlist, x => x.Username == createAdminCommand.Username);
    }
    [Fact]
    public async Task DeleteAdmin_ExistingAdmin_ShouldReturnHttpNoContent()
    {
        //prepare
        await client.Authenticate();
        CreateAdminCommand createAdminCommand = new()
        {
            Email = Faker.Internet.Email(),
            Name = "SomeSpecificName",
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),
        };
        var createAdminResult = await client.SendAsync<CreateAdminCommandResponse>("api/v1/Admin", Helper.CreateJsonContent(createAdminCommand), HttpMethod.Post); //create an admin    
        DeleteAdminCommand DeleteAdminCommand = new()
        {
            Id = createAdminResult.Id
        };

        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(DeleteAdminCommand), HttpMethod.Delete); //get its information

        //validate
        Assert.Equal(System.Net.HttpStatusCode.NoContent, result.StatusCode);

    }
    [Fact]
    public async Task DeleteAdmin_NotExistingAdmin_ShouldReturnHttpNotFound()
    {
        //prepare
        await client.Authenticate();
        DeleteAdminCommand DeleteAdminCommand = new()
        {
            Id = Guid.NewGuid(),
        };

        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(DeleteAdminCommand), HttpMethod.Delete); //get its information

        //validate
        Assert.Equal(System.Net.HttpStatusCode.NotFound, result.StatusCode);

    }
    [Fact]
    public async Task AddAdmin_UniqueValues_ShouldReturnHttpOKAndValidGuid()
    {
        //prepare
        await client.Authenticate();
        CreateAdminCommand createAdminCommand = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };

        //execute
        var result = await client.PostAsJsonAsync("api/v1/Admin", createAdminCommand);

        //validate
        Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
    }


    [Fact]
    public async Task AddAdmin_DuplicateUserName_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateAdminCommand createAdminCommand = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };
        CreateAdminCommand createAdminCommand2 = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = createAdminCommand.Username,

        };

        //execute
        await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(createAdminCommand), HttpMethod.Post);

        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(createAdminCommand2), HttpMethod.Post);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task AddAdmin_DuplicateEmail_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateAdminCommand createAdminCommand = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };
        CreateAdminCommand createAdminCommand2 = new()
        {
            Email = createAdminCommand.Email,
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };

        //execute
        await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(createAdminCommand), HttpMethod.Post);

        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(createAdminCommand2), HttpMethod.Post);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task AddAdmin_PasswordLessThan8Char_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateAdminCommand createAdminCommand = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(7),
            Username = Faker.Internet.UserName(),

        };

        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(createAdminCommand), HttpMethod.Post);

        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task AddAdmin_UsernameIsNull_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateAdminCommand createAdminCommand = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(7),
            Username = null,

        };

        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(createAdminCommand), HttpMethod.Post);

        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task AddAdmin_EmailIsNull_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateAdminCommand createAdminCommand = new()
        {
            Email = null,
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(7),
            Username = Faker.Internet.UserName(),

        };

        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(createAdminCommand), HttpMethod.Post);

        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task AddAdmin_NameIsNull_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateAdminCommand createAdminCommand = new()
        {
            Email = Faker.Internet.Email(),
            Name = null,
            Password = Faker.Internet.Password(7),
            Username = Faker.Internet.UserName(),

        };

        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(createAdminCommand), HttpMethod.Post);

        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
}
