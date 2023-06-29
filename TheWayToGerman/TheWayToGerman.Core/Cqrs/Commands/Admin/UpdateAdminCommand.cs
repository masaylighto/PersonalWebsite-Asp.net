
using Core.Cqrs.Requests;
using Core.DataKit;
using TheWayToGerman.Core.Enums;

namespace TheWayToGerman.Core.Cqrs.Commands.Admin;

public class UpdateAdminCommand : ICommand<OK>
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}
