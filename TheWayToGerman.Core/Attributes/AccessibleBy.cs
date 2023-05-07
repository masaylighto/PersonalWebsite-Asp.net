using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

using TheWayToGerman.Core.Enums;
using TheWayToGerman.Core.Helpers;

namespace TheWayToGerman.Core.Attributes
{

    /// <summary>
    /// this class detect if the user has the permissions to access the route
    /// by comparing the route required permissions to the one in the RouteValues which filed by the middleware that responsible for  AUTH process  
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AccessibleBy : Attribute, IResourceFilter
    {
        private UserType[] UsersType { get; init; }
        public AccessibleBy(params UserType[] usersType)
        {
            UsersType = usersType;
        }
        public object? GetUserType(ResourceExecutingContext context)
        {
            object routeValue;
            context.HttpContext.Items.TryGetValue(Constants.UserTypeKey, out routeValue!);
            return routeValue;
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            object? UserType = GetUserType(context);
            if (UserType is null)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                throw new UnauthorizedAccessException("Insufficient permissions");

            }
            if (!UsersType.Contains(Enum.Parse<UserType>(UserType.ToString()!)))
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                throw new UnauthorizedAccessException("Insufficient permissions");
            }

        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {

        }
    }
}
