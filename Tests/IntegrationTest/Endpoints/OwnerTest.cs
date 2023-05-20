

using Bogus;
using System.Net.Http.Json;
using TheWayToGerman.Api.DTO;
using TheWayToGerman.Api.DTO.Owner;
using TheWayToGerman.Api.ResponseObject.Owner;
using TheWayToGerman.Core.Cqrs.Queries;
using TheWayToGerman.Core.Entities;

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
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

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
        await client.SendAsync("v1/Owner/Admin", Helper.CreateJsonContent(createAdminDTO), HttpMethod.Post);

        var result = await client.SendAsync("v1/Owner/Admin", Helper.CreateJsonContent(createAdminDTO2), HttpMethod.Post);
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
        var result = await client.SendAsync("v1/Owner", Helper.CreateJsonContent(updateUserInformationDTO), HttpMethod.Put);
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

        var result = await client.SendAsync("v1/Owner", Helper.CreateJsonContent(updateUserInformationDTO2), HttpMethod.Put);
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

        var result = await client.SendAsync("v1/Owner", Helper.CreateJsonContent(updateUserInformationDTO), HttpMethod.Put);
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
        var result = await client.SendAsync("v1/Owner", Helper.CreateJsonContent(updateUserInformationDTO), HttpMethod.Put);

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
        var result = await client.SendAsync("v1/Owner", Helper.CreateJsonContent(updateUserInformationDTO), HttpMethod.Put);

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
        var result = await client.SendAsync("v1/Owner", Helper.CreateJsonContent(updateUserInformationDTO), HttpMethod.Put);

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
        var result = await client.SendAsync("v1/Owner", Helper.CreateJsonContent(updateUserInformationDTO), HttpMethod.Put);

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
        await client.SendAsync("v1/Owner", Helper.CreateJsonContent(updateUserInformationDTO), HttpMethod.Put); //the first update request because we don't know what the default value or the previous value are so we set them to something then try to update to the same thing
        var result = await client.SendAsync("v1/Owner", Helper.CreateJsonContent(updateUserInformationDTO), HttpMethod.Put);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Fact]
    public async Task GetAdmins_NonExistingName_ShouldReturnHttpOkWithEmptyArray()
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
        GetAdminsDTO GetAdminDTO = new()
        {           
            Name = "SomeRandomName",
        };
        await client.SendAsync("v1/Owner/Admin", Helper.CreateJsonContent(createAdminDTO), HttpMethod.Post);
        var result = await client.SendAsync("v1/Owner/Admin", Helper.CreateJsonContent(GetAdminDTO), HttpMethod.Get);
        //execute
        //validate
        Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        var userlist =await result.Content.ReadFromJsonAsync<IEnumerable<GetAdminsQueryResponse>>();
        Assert.NotNull(userlist);
        Assert.Empty(userlist);
    }
    [Fact]
    public async Task GetAdmins_ExistingName_ShouldReturnHttpOkWithArrayOfMatchedUser()
    {
        //prepare
        await client.Authenticate();
        CreateAdminDTO createAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = "SomeSpecificName",
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };
        GetAdminsDTO GetAdminDTO = new()
        {
            Name = "SomeSpecificName",
        };
        await client.SendAsync("v1/Owner/Admin", Helper.CreateJsonContent(createAdminDTO), HttpMethod.Post);
        var result = await client.SendAsync("v1/Owner/Admin", Helper.CreateJsonContent(GetAdminDTO), HttpMethod.Get);
        //execute
        //validate
        Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        var userlist = await result.Content.ReadFromJsonAsync<IEnumerable<GetAdminsQueryResponse>>();
        Assert.NotNull(userlist);
        Assert.Contains(userlist, x => x.Email == createAdminDTO.Email);
        Assert.Contains(userlist, x => x.Name  == createAdminDTO.Name);
        Assert.Contains(userlist, x => x.Username  == createAdminDTO.Username);
    }
    [Fact]
    public async Task DeleteAdmin_ExistingAdmin_ShouldReturnHttpNoContent()
    {
        //prepare
        await client.Authenticate();
        CreateAdminDTO createAdminDTO = new()
        {            
            Email = Faker.Internet.Email(),
            Name = "SomeSpecificName",
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),
        };
        await client.SendAsync("v1/Owner/Admin", Helper.CreateJsonContent(createAdminDTO), HttpMethod.Post); //create an admin
        var admins = await client.SendAsync<IEnumerable<GetAdminsResponse>>("v1/Owner/Admin", Helper.CreateJsonContent(createAdminDTO), HttpMethod.Get); //get its information
        DeleteAdminDTO DeleteAdminDTO = new()
        {
            Id = admins.FirstOrDefault().Id
        };

        //execute
        var result = await client.SendAsync("v1/Owner/Admin", Helper.CreateJsonContent(DeleteAdminDTO), HttpMethod.Delete); //get its information

        //validate
        Assert.Equal(System.Net.HttpStatusCode.NoContent, result.StatusCode);

    }
    [Fact]
    public async Task DeleteAdmin_NotExistingAdmin_ShouldReturnHttpNotFound()
    {
        //prepare
        await client.Authenticate();
        DeleteAdminDTO DeleteAdminDTO = new()
        {
            Id = Guid.NewGuid(),
        };

        //execute
        var result = await client.SendAsync("v1/Owner/Admin", Helper.CreateJsonContent(DeleteAdminDTO), HttpMethod.Delete); //get its information

        //validate
        Assert.Equal(System.Net.HttpStatusCode.NotFound, result.StatusCode);

    }




    public async Task AddOwner_UniqueValues_ShouldReturnHttpOK()
    {
        //prepare
        CreateFirstOwnerDTO DTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),
        };

        //execute
        var result = await client.PostAsJsonAsync("v1/Owner", DTO);

        //validate
        Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
    }


    [Fact]
    public async Task AddOwner_OwnerAlreadyExist_ShouldReturnHttpBadRequest()
    {
        //prepare
        CreateFirstOwnerDTO createAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };
        CreateFirstOwnerDTO createOwnerDTO2 = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };

        //execute
        await client.SendAsync("v1/Owner", Helper.CreateJsonContent(createAdminDTO), HttpMethod.Post);

        var result = await client.SendAsync("v1/Owner", Helper.CreateJsonContent(createOwnerDTO2), HttpMethod.Post);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task AddOwner_PasswordLessThan8Char_ShouldReturnHttpBadRequest()
    {
        //prepare
        CreateAdminDTO createOwnerDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(7),
            Username = Faker.Internet.UserName(),

        };

        //execute
        var result = await client.SendAsync("v1/Owner", Helper.CreateJsonContent(createOwnerDTO), HttpMethod.Post);

        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task AddOwner_UsernameIsNull_ShouldReturnHttpBadRequest()
    {
        //prepare
        CreateFirstOwnerDTO createfirstOwnerDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(7),
            Username = null,

        };

        //execute
        var result = await client.SendAsync("v1/Owner", Helper.CreateJsonContent(createfirstOwnerDTO), HttpMethod.Post);

        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task AddOwner_EmailIsNull_ShouldReturnHttpBadRequest()
    {
        //prepare      
        CreateFirstOwnerDTO createFirstOwnerDTO = new()
        {
            Email = null,
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(7),
            Username = Faker.Internet.UserName(),

        };

        //execute
        var result = await client.SendAsync("v1/Owner", Helper.CreateJsonContent(createFirstOwnerDTO), HttpMethod.Post);

        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task AddOwner_NameIsNull_ShouldReturnHttpBadRequest()
    {
        //prepare
        CreateFirstOwnerDTO createFirstOwnerDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = null,
            Password = Faker.Internet.Password(7),
            Username = Faker.Internet.UserName(),

        };
        //execute
        var result = await client.SendAsync("v1/Owner", Helper.CreateJsonContent(createFirstOwnerDTO), HttpMethod.Post);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }

}
