using Core.Cqrs.Requests;
using TheWayToGerman.Core.Entities;

namespace TheWayToGerman.Core.Cqrs.Queries;

public class AuthenticationQuery : IQuery<User>
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}
