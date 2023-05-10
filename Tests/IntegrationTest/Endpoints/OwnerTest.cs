

using Bogus;
using Humanizer;
using System.Net.Http.Json;
using TheWayToGerman.Api.DTO;
using TheWayToGerman.Api.DTO.Login;
using TheWayToGerman.Api.DTO.Owner;
using TheWayToGerman.Api.ResponseObject.Login;

namespace IntegrationTest.Features;
[Collection("Sequential")]
public class OwnerTest
{
    readonly Faker Faker = new Faker();
    readonly HttpClient client;

    public OwnerTest()
    {
        client = WebApplicationBuilder.ApiClient();
    }
    [Fact]
    public async Task AddAdmin_UniqueValues_ShouldReturnHttpOK()
    {
        //prepare
        await client.Authenticate();
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
    public async Task AddAdmin_DuplicateUserName_ShouldReturnHttpBadRequest()
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
            Username = createAdminDTO.Username,

        };
        
        //execute
         await client.SendAsync("v1/Owner/Admin", Helper.CreateJsonContent(createAdminDTO),HttpMethod.Post);
        
         var result = await client.SendAsync("v1/Owner/Admin", Helper.CreateJsonContent(createAdminDTO2),HttpMethod.Post);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task AddAdmin_DuplicateEmail_ShouldReturnHttpBadRequest()
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
            Email = createAdminDTO.Email,
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };

        //execute
        await client.SendAsync("v1/Owner/Admin", Helper.CreateJsonContent(createAdminDTO), HttpMethod.Post);

        var result = await client.SendAsync("v1/Owner/Admin", Helper.CreateJsonContent(createAdminDTO2), HttpMethod.Post);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task AddAdmin_PasswordLessThan8Char_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateAdminDTO createAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(7),
            Username = Faker.Internet.UserName(),

        };

        //execute
        var result = await client.SendAsync("v1/Owner/Admin", Helper.CreateJsonContent(createAdminDTO), HttpMethod.Post);

        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task AddAdmin_UsernameIsNull_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateAdminDTO createAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(7),
            Username = null,

        };

        //execute
        var result = await client.SendAsync("v1/Owner/Admin", Helper.CreateJsonContent(createAdminDTO), HttpMethod.Post);

        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task AddAdmin_EmailIsNull_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateAdminDTO createAdminDTO = new()
        {
            Email = null,
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(7),
            Username = Faker.Internet.UserName(),

        };

        //execute
        var result = await client.SendAsync("v1/Owner/Admin", Helper.CreateJsonContent(createAdminDTO), HttpMethod.Post);

        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task AddAdmin_NameIsNull_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateAdminDTO createAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = null,
            Password = Faker.Internet.Password(7),
            Username = Faker.Internet.UserName(),

        };

        //execute
        var result = await client.SendAsync("v1/Owner/Admin", Helper.CreateJsonContent(createAdminDTO), HttpMethod.Post);

        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Fact]
    public async Task UpdateOwner_CorrectInformation_ShouldReturnOK()
    {
        //prepare
        await client.Authenticate();
        UpdateUserInformationDTO updateUserInformationDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Person.FirstName,
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };

        //execute
        var result = await client.SendAsync("v1/Owner/Self", Helper.CreateJsonContent(updateUserInformationDTO), HttpMethod.Put); 
        //validate
        Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
    }




    [Fact]
    public async Task UpdateOwner_DuplicateUserName_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateAdminDTO CreateAdminDto = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };
        UpdateUserInformationDTO updateUserInformationDTO2 = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = CreateAdminDto.Username,

        };

        //execute
        await client.SendAsync("v1/Owner/Admin", Helper.CreateJsonContent(CreateAdminDto), HttpMethod.Post);

        var result = await client.SendAsync("v1/Owner/Self", Helper.CreateJsonContent(updateUserInformationDTO2), HttpMethod.Put);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task UpdateOwner_DuplicateEmail_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateAdminDTO CreateAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };
        UpdateUserInformationDTO updateUserInformationDTO = new()
        {
            Email = CreateAdminDTO.Email,
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };

        //execute
        await client.SendAsync("v1/Owner/Admin", Helper.CreateJsonContent(CreateAdminDTO), HttpMethod.Post);

        var result = await client.SendAsync("v1/Owner/Self", Helper.CreateJsonContent(updateUserInformationDTO), HttpMethod.Put);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task UpdateOwner_PasswordLessThan8Char_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        UpdateUserInformationDTO updateUserInformationDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(7),
            Username = Faker.Internet.UserName(),

        };

        //execute
        var result = await client.SendAsync("v1/Owner/Self", Helper.CreateJsonContent(updateUserInformationDTO), HttpMethod.Put);

        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task UpdateOwner_UsernameIsNull_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        UpdateUserInformationDTO updateUserInformationDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(7),
            Username = null,

        };

        //execute
        var result = await client.SendAsync("v1/Owner/Self", Helper.CreateJsonContent(updateUserInformationDTO), HttpMethod.Put);

        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task UpdateOwner_EmailIsNull_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        UpdateUserInformationDTO updateUserInformationDTO = new()
        {
            Email = null,
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };

        //execute
        var result = await client.SendAsync("v1/Owner/self", Helper.CreateJsonContent(updateUserInformationDTO), HttpMethod.Put);

        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task UpdateOwner_NameIsNull_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        UpdateUserInformationDTO updateUserInformationDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = null,
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };

        //execute
        var result = await client.SendAsync("v1/Owner/Self", Helper.CreateJsonContent(updateUserInformationDTO), HttpMethod.Put);

        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task UpdateOwner_NoDataChanges_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        UpdateUserInformationDTO updateUserInformationDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Person.FirstName,
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),
        };

        //execute
        await client.SendAsync("v1/Owner/Self", Helper.CreateJsonContent(updateUserInformationDTO), HttpMethod.Put); //the first update request because we don't know what the default value or the previous value are so we set them to something then try to update to the same thing
        var result = await client.SendAsync("v1/Owner/Self", Helper.CreateJsonContent(updateUserInformationDTO), HttpMethod.Put);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
}
