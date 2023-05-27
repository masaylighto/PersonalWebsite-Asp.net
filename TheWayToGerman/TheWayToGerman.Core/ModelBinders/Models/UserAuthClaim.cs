
using Microsoft.AspNetCore.Http;
using System.Reflection;
using System;
using TheWayToGerman.Core.Enums;
using TheWayToGerman.Core.ParametersBinders;
using Microsoft.AspNetCore.Mvc;

namespace TheWayToGerman.Core.ModelBinders.Models;

[ModelBinder(typeof(UserAuthClaimBinder))]
public class UserAuthClaim
{
    public Guid ID { get; set; }
    public UserType UserType { get; set; }
}
