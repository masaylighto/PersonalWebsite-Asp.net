
using TheWayToGerman.Core.Attributes;
using TheWayToGerman.Core.Enums;

namespace UnitTest.Core.Attributes;

public class AccessibleByTest
{
    [Fact]
    public void CheckIfUserIsAllowed_PassedUserIsNotAllowed_ShouldReturnFalse()
    {
        AccessibleBy accessibleBy = new AccessibleBy(UserType.Owner);
        Assert.False(accessibleBy.IsUserTypeAllowed(UserType.Admin));
    }
    [Fact]
    public void CheckIfUserIsAllowed_PassedUserIsAllowed_ShouldReturnTrue()
    {
        AccessibleBy accessibleBy = new AccessibleBy(UserType.Owner);
        Assert.True(accessibleBy.IsUserTypeAllowed(UserType.Owner));
    }
    [Fact]
    public void CheckIfUserIsAllowed_PassedUserIsNull_ShouldReturnFalse()
    {
        AccessibleBy accessibleBy = new AccessibleBy(UserType.Owner);
        Assert.False(accessibleBy.IsUserTypeAllowed(null));
    }
    [Fact]
    public void CheckIfUserIsAllowed_PassingADifferentDataType_ShouldReturnFalse()
    {
        AccessibleBy accessibleBy = new AccessibleBy(UserType.Owner);
        Assert.False(accessibleBy.IsUserTypeAllowed(new string("")));
    }
}
