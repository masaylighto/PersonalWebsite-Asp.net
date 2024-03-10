using Core.Cqrs.Requests;
using PersonalWebsiteApi.Core.Entities;

namespace PersonalWebsiteApi.Core.Cqrs.Queries;

public class AuthenticationQuery : IQuery<User>
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}
