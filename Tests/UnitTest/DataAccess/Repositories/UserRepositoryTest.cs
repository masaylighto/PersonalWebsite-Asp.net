
using Bogus;
using Core.DataKit;
using Core.DataKit.MockWrapper;
using Moq;
using System.Runtime.Intrinsics.X86;
using TheWayToGerman.Core.Database;
using TheWayToGerman.Core.Entities;
using TheWayToGerman.Core.Exceptions;
using TheWayToGerman.Core.Loggers;
using TheWayToGerman.DataAccess.Interfaces;
using TheWayToGerman.DataAccess.Repositories;

namespace UnitTest.DataAccess.Repositories;

public class UserRepositoryTest
{
    readonly Faker FakeData = new Faker();

    readonly Faker<User> FakeUser = new Faker<User>().CustomInstantiator((faker) =>
    {
        return new User()
        {
            Email = faker.Person.Email,
            Name = faker.Name.FirstName(),
            Username = faker.Person.UserName,
            UserType = TheWayToGerman.Core.Enums.UserType.Admin,
        };
    });
    readonly PostgresDBContext postgresDB;
    readonly IUserRepository UserRespository;
    public UserRepositoryTest()
    {
        postgresDB = FakeDBContext.Create();
        Mock<ILog> logMock = new Mock<ILog>();
        UserRespository = new UserRepository(postgresDB, new DateTimeProvider(), logMock.Object);
    }
    [Fact]
    public async Task AddUser_CorrectInformation_ShouldBeAdded()
    {
        //Prepare
        User testUser = FakeUser.Generate();
        testUser.SetPassword(FakeData.Internet.Password());

        //Execute
        await UserRespository.AddUserAsync(testUser);
        int changes = postgresDB.SaveChanges();

        //Validate
        Assert.Equal(1, changes);
    }


    [Fact]
    public async Task IsUserExist_UserExist_ShouldReturnTrue()
    {
        //Prepare
        User testUser = FakeUser.Generate();
        testUser.SetPassword(FakeData.Internet.Password());        
        await UserRespository.AddUserAsync(testUser);
        postgresDB.SaveChanges();

        //Execute
        var exist =await UserRespository.IsUserExistAsync(x => x.Id == testUser.Id);
        //Validate
        Assert.True(exist.IsDataType<OK>());
    }
    [Fact]
    public async Task IsUserExist_UserNotExist_ShouldReturnFalse()
    {
        //Prepare
        User testUser = FakeUser.Generate();
        testUser.SetPassword(FakeData.Internet.Password());
        await UserRespository.AddUserAsync(testUser);
        postgresDB.SaveChanges();

        //Execute
        var exist = await UserRespository.IsUserExistAsync(x => x.Id == Guid.NewGuid());
        //Validate
        Assert.False(exist.IsDataType<OK>());
    }
    [Fact]
    public async Task AddUser_NullPassword_ShouldReturnArgumentNullException()
    {
        //Prepare
        User user = FakeUser.Generate();

        //Execute
        var result = await UserRespository.AddUserAsync(user);

        //Validate
        Assert.True(result.IsErrorType<NullValueException>());
        Assert.Equal(0, postgresDB.SaveChanges());
    }
    [Fact]
    public async Task AddUser_DublicateEmail_ShouldReturnUniqueFieldException()
    {
        //Prepare
        User user1 = FakeUser.Generate(),
             user2 = FakeUser.Generate();
        user1.Email = user2.Email;
        user1.SetPassword(FakeData.Internet.Password());
        user2.SetPassword(FakeData.Internet.Password());

        //Execute
        await UserRespository.AddUserAsync(user1);
        postgresDB.SaveChanges();
        var result = await UserRespository.AddUserAsync(user2);
        int changes = postgresDB.SaveChanges();

        //Validate
        Assert.True(result.IsErrorType<UniqueFieldException>());
        Assert.Equal(0, changes);
    }
    [Fact]
    public async Task AddUser_DublicateUserName_ShouldReturnUniqueFieldException()
    {   //Prepare
        User user1 = FakeUser.Generate(), user2 = FakeUser.Generate();
        user1.Username = user2.Username;
        user1.SetPassword(FakeData.Internet.Password());
        user2.SetPassword(FakeData.Internet.Password());

        //Execute
        await UserRespository.AddUserAsync(user1);
        postgresDB.SaveChanges();
        var result = await UserRespository.AddUserAsync(user2);
        int changes = postgresDB.SaveChanges();

        //Validate
        Assert.True(result.IsErrorType<UniqueFieldException>());
        Assert.Equal(0, changes);
    }


    [Fact]
    public async Task UpdateUser_MoreThanOneMatch_ShouldReturnNullValueException()
    {   //Prepare
        User user1 = FakeUser.Generate(), user2 = FakeUser.Generate(), updatedUser = FakeUser.Generate();
        user1.UserType = TheWayToGerman.Core.Enums.UserType.Owner;
        user2.UserType = TheWayToGerman.Core.Enums.UserType.Owner;
        updatedUser.UserType = TheWayToGerman.Core.Enums.UserType.Owner;
        user1.SetPassword(FakeData.Internet.Password());
        user2.SetPassword(FakeData.Internet.Password());
        await UserRespository.AddUserAsync(user1);
        await UserRespository.AddUserAsync(user2);
        postgresDB.SaveChanges();
        //Execute
        var result = await UserRespository.UpdateUserAsync(updatedUser, x => x.UserType == TheWayToGerman.Core.Enums.UserType.Owner);
        int changes = postgresDB.SaveChanges();

        //Validate
        Assert.True(result.ContainError());
        Assert.Equal(0, changes);
    }
    [Fact]
    public async Task UpdateUser_OneMatch_ShouldReturnUpdate()
    {  
        //Prepare
        User user1 = FakeUser.Generate(), user2 = FakeUser.Generate(), updatedUser = FakeUser.Generate();
        user1.UserType = TheWayToGerman.Core.Enums.UserType.Owner;
        user2.UserType = TheWayToGerman.Core.Enums.UserType.Admin;
        updatedUser.UserType = TheWayToGerman.Core.Enums.UserType.Owner;
        user1.SetPassword(FakeData.Internet.Password());
        user2.SetPassword(FakeData.Internet.Password());
        await UserRespository.AddUserAsync(user1);
        await UserRespository.AddUserAsync(user2);
        postgresDB.SaveChanges();
        //Execute
        var result = await UserRespository.UpdateUserAsync(updatedUser, x => x.UserType == TheWayToGerman.Core.Enums.UserType.Owner);
        int changes = postgresDB.SaveChanges();

        //Validate
        Assert.False(result.ContainError());
        Assert.Equal(1, changes);
    }
    [Fact]
    public async Task DeleteAdmin_AdminExist_ShouldReturnOK()
    {
        //Prepare
        User user = FakeUser.Generate();
        user.SetPassword(FakeData.Internet.Password(8));
        await UserRespository.AddUserAsync(user);
        postgresDB.SaveChanges();
        //Execute
        var result = await UserRespository.DeleteAdminById(user.Id);
        int changes = postgresDB.SaveChanges();

        //Validate
        Assert.False(result.ContainError());
        Assert.Equal(1, changes);
    }
    [Fact]
    public async Task DeleteAdmin_AdminNotExist_ShouldReturnOK()
    {
        //Prepare
        //Execute
        var result = await UserRespository.DeleteAdminById(Guid.NewGuid());
        int changes = postgresDB.SaveChanges();

        //Validate
        Assert.True(result.ContainError());
        Assert.Equal(0, changes);
    }
}
