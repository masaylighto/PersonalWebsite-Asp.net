#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
using Bogus;
using System.Net.Http.Json;
using TheWayToGerman.Core.Cqrs.Commands;
using TheWayToGerman.Core.Cqrs.Commands.Admin;

namespace IntegrationTest.Endpoints;
public class OwnerTest
{
    readonly Faker Faker = new Faker();
    readonly HttpClient client;

    public OwnerTest()
    {
        client = WebApplicationBuilder.ApiClient();
    }


    [Fact]
    public async Task UpdateOwner_CorrectInformation_ShouldReturnOK()
    {
        //prepare
        await client.Authenticate();
        UpdateOwnerInformationCommand updateUserInformationDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Person.FirstName,
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };

        //execute
        var result = await client.SendAsync("api/v1/Owner", Helper.CreateJsonContent(updateUserInformationDTO), HttpMethod.Put);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
    }




    [Fact]
    public async Task UpdateOwner_DuplicateUserName_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateAdminCommand CreateAdminDto = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };
        UpdateOwnerInformationCommand updateUserInformationDTO2 = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = CreateAdminDto.Username,

        };

        //execute
        await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(CreateAdminDto), HttpMethod.Post);

        var result = await client.SendAsync("api/v1/Owner", Helper.CreateJsonContent(updateUserInformationDTO2), HttpMethod.Put);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task UpdateOwner_DuplicateEmail_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateAdminCommand CreateAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };
        UpdateOwnerInformationCommand updateUserInformationDTO = new()
        {
            Email = CreateAdminDTO.Email,
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };

        //execute
        await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(CreateAdminDTO), HttpMethod.Post);

        var result = await client.SendAsync("api/v1/Owner", Helper.CreateJsonContent(updateUserInformationDTO), HttpMethod.Put);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task UpdateOwner_PasswordLessThan8Char_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        UpdateOwnerInformationCommand updateUserInformationDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(7),
            Username = Faker.Internet.UserName(),

        };

        //execute
        var result = await client.SendAsync("api/v1/Owner", Helper.CreateJsonContent(updateUserInformationDTO), HttpMethod.Put);

        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task UpdateOwner_UsernameIsNull_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();

        UpdateOwnerInformationCommand updateUserInformationDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(7),
            Username = null,

        };

        //execute
        var result = await client.SendAsync("api/v1/Owner", Helper.CreateJsonContent(updateUserInformationDTO), HttpMethod.Put);

        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task UpdateOwner_EmailIsNull_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        UpdateOwnerInformationCommand updateUserInformationDTO = new()
        {
            Email = null,
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };

        //execute
        var result = await client.SendAsync("api/v1/Owner", Helper.CreateJsonContent(updateUserInformationDTO), HttpMethod.Put);

        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task UpdateOwner_NameIsNull_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        UpdateOwnerInformationCommand updateUserInformationDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = null,
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };

        //execute
        var result = await client.SendAsync("api/v1/Owner", Helper.CreateJsonContent(updateUserInformationDTO), HttpMethod.Put);

        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task UpdateOwner_NoDataChanges_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        UpdateOwnerInformationCommand updateUserInformationDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Person.FirstName,
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),
        };

        //execute
        await client.SendAsync("api/v1/Owner", Helper.CreateJsonContent(updateUserInformationDTO), HttpMethod.Put); //the first update request because we don't know what the default value or the previous value are so we set them to something then try to update to the same thing
        var result = await client.SendAsync("api/v1/Owner", Helper.CreateJsonContent(updateUserInformationDTO), HttpMethod.Put);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }




    [Fact]
    public async Task AddOwner_UniqueValues_ShouldReturnHttpOK()
    {
        //prepare
        CreateFirstOwnerCommand DTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),
        };

        //execute
        var result = await client.PostAsJsonAsync("api/v1/Owner", DTO);

        //validate
        Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
    }


    [Fact]
    public async Task AddOwner_OwnerAlreadyExist_ShouldReturnHttpBadRequest()
    {
        //prepare
        CreateFirstOwnerCommand createOwnerDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };
        CreateFirstOwnerCommand createOwnerDTO2 = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };

        //execute
        await client.SendAsync("api/v1/Owner", Helper.CreateJsonContent(createOwnerDTO), HttpMethod.Post);

        var result = await client.SendAsync("api/v1/Owner", Helper.CreateJsonContent(createOwnerDTO2), HttpMethod.Post);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task AddOwner_PasswordLessThan8Char_ShouldReturnHttpBadRequest()
    {
        //prepare
        CreateAdminCommand createOwnerDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(7),
            Username = Faker.Internet.UserName(),

        };

        //execute
        var result = await client.SendAsync("api/v1/Owner", Helper.CreateJsonContent(createOwnerDTO), HttpMethod.Post);

        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task AddOwner_UsernameIsNull_ShouldReturnHttpBadRequest()
    {
        //prepare
        CreateFirstOwnerCommand createfirstOwnerDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(7),
            Username = null,

        };

        //execute
        var result = await client.SendAsync("api/v1/Owner", Helper.CreateJsonContent(createfirstOwnerDTO), HttpMethod.Post);

        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task AddOwner_EmailIsNull_ShouldReturnHttpBadRequest()
    {
        //prepare      
        CreateFirstOwnerCommand createFirstOwnerDTO = new()
        {
            Email = null,
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(7),
            Username = Faker.Internet.UserName(),

        };

        //execute
        var result = await client.SendAsync("api/v1/Owner", Helper.CreateJsonContent(createFirstOwnerDTO), HttpMethod.Post);

        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task AddOwner_NameIsNull_ShouldReturnHttpBadRequest()
    {
        //prepare
        CreateFirstOwnerCommand createFirstOwnerDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = null,
            Password = Faker.Internet.Password(7),
            Username = Faker.Internet.UserName(),

        };
        //execute
        var result = await client.SendAsync("api/v1/Owner", Helper.CreateJsonContent(createFirstOwnerDTO), HttpMethod.Post);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }

}
