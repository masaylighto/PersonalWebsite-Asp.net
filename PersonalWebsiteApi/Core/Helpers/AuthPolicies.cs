

using PersonalWebsiteApi.Core.Enums;

namespace PersonalWebsiteApi.Core.Helpers;

public static class AuthPolicies
{
    public const string OwnerPolicy = nameof(UserType.Owner);
    public const string AdminPolicy = nameof(UserType.Admin);
    public const string AdminsAndOwners = nameof(AdminsAndOwners);
}
