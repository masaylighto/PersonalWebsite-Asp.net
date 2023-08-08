
using Microsoft.AspNetCore.Mvc;
using TheWayToGerman.Core.Enums;
using TheWayToGerman.Core.ParametersBinders;

namespace TheWayToGerman.Core.ModelBinders.Models;

[ModelBinder(typeof(UserAuthClaimBinder))]
public class UserAuthClaim
{
    public Guid ID { get; set; }
    public UserType UserType { get; set; }
}
