using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using TheWayToGerman.Core.Enums;
using TheWayToGerman.Core.Helpers;

namespace TheWayToGerman.Core.Attributes
{

    /// <summary>
    /// this class detect if the user has the permissions to access the route
    /// by comparing the route required permissions to the one in the RouteValues which filed by the middleware that responsible for  AUTH process  
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AccessibleByAttribute : Attribute, IResourceFilter
    {
        UserType[] AllowedUsersTypes { get; init; }
        public AccessibleByAttribute(params UserType[] allowedusersTypes)
        {
            AllowedUsersTypes = allowedusersTypes;
        }
        public object? GetUserType(ResourceExecutingContext context)
        {
            return context.HttpContext.GetRouteValue(Constants.UserTypeKey); 
        }
        public bool IsUserTypeAllowed(object? userType)
        {
            if (userType is null)
            {
                return false;
            }
            UserType type;
            if (!Enum.TryParse(userType.ToString(), out type))
            {
                return false;
            } 
            return AllowedUsersTypes.Contains(type);

        }
        public void OnResourceExecuting(ResourceExecutingContext context)
        {

            object? UserType = GetUserType(context);
            if (!IsUserTypeAllowed(UserType))
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.HttpContext.Response.WriteAsync("Unauthorized access");
                
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
           //excuted after method call and we are in no need to do anything at that time
        }
    }
}
