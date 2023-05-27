
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;
using TheWayToGerman.Core.Enums;
using TheWayToGerman.Core.Helpers;
using TheWayToGerman.Core.ModelBinders.Models;

namespace TheWayToGerman.Core.ParametersBinders;

public class UserAuthClaimBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
       var userClaims = bindingContext.HttpContext.User.Claims;
       var userRole = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role);
       var userID = userClaims.FirstOrDefault(x => x.Type == Constants.UserIDKey);
       if (userRole is null || userID is null)
       {
          return Task.CompletedTask;
       }
       bindingContext.ModelState.SetModelValue("UserType", userRole.Value, "");
       bindingContext.ModelState.SetModelValue("ID", userID.Value, "");              
       bindingContext.Result = ModelBindingResult.Success(new UserAuthClaim
       {
           ID= Guid.Parse(userID.Value),
           UserType = Enum.Parse<UserType>(userRole.Value)
       });
       return Task.CompletedTask;
    }

}
