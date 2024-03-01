
using Microsoft.AspNetCore.Mvc;
using PersonalWebsiteApi.Core.Enums;
using PersonalWebsiteApi.Core.ParametersBinders;

namespace PersonalWebsiteApi.Core.ModelBinders.Models;

[ModelBinder(typeof(UserAuthClaimBinder))]
public class UserAuthClaim
{
    public Guid ID { get; set; }
    public UserType UserType { get; set; }
}
