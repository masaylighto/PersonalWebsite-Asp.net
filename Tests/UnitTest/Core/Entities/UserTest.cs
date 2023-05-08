
using Bogus;
using TheWayToGerman.Core.Entities;

namespace UnitTest.Core.Entities;

public class UserTest
{
    readonly Faker FakeData= new Faker();

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
    [Fact]
    public void CreateUser_PasswordAreNotEqual_ShouldReturnFalse()
    {
        //prepare
        User user = FakeUser.Generate();
        user.SetPassword(FakeData.Internet.Password());

        //Execute
        bool result = user.IsPasswordEqual(FakeData.Internet.Password());

        //Validate
        Assert.False(result);
    }
    [Fact]
    public void CreateUser_PasswordAreEqual_ShouldReturnTrue()
    {   //prepare
        var passowrd = FakeData.Internet.Password();
        User user = FakeUser.Generate();
        user.SetPassword(passowrd);
        //Execute
        bool result = user.IsPasswordEqual(passowrd);

        //Validate
        Assert.True(result);
    }
}
