

using TheWayToGerman.Core.Enums;

namespace TheWayToGerman.Core.Helpers;

public static class AuthPolicies
{
    public const string OwnerPolicy = nameof(UserType.Owner);
    public const string AdminPolicy = nameof(UserType.Admin);
    public const string AdminsAndOwners = nameof(AdminsAndOwners);
}
