using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheWayToGerman.Core.Entities;
using TheWayToGerman.Core.Enums;

namespace TheWayToGerman.Core.Database;

public class UserTableConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {

        var user = new User
        {
            Id = new Guid("4413e488-a54e-43df-b381-f48fc81b7080"),
            Email = "masaylighto@gmail.com",
            Name = "mohammed",
            Username = "masaylighto",
            UserType = UserType.Owner
        };
        user.SetPassword("8PS33gotf24a");
        builder.HasData(user);
    }

}
