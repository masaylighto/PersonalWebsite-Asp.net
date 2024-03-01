
using Core.Cqrs.Requests;
using Core.DataKit;

namespace PersonalWebsiteApi.Core.Cqrs.Commands;

public class CreateFirstOwnerCommand : ICommand<OK>
{
    public required string Name { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}
