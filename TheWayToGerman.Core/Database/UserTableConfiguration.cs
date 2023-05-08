using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheWayToGerman.Core.Entities;
using TheWayToGerman.Core.Enums;

namespace TheWayToGerman.Core.Database;

public class UserTableConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {

        var user = new User {
            Id= Guid.NewGuid(),
            Email = "masaylighto@gmail.com",
            Name = "mohammed",
            Username = "masaylighto",
            UserType = UserType.Owner 
        };
        user.SetPassword("8PS33gotf24a");
        builder.HasData(user);
    }
    
}
