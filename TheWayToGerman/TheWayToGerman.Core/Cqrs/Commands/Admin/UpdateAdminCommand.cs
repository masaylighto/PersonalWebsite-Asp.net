
using Core.Cqrs.Requests;
using Core.DataKit;
using System.Text.Json.Serialization;

namespace TheWayToGerman.Core.Cqrs.Commands.Admin;

public class UpdateAdminCommand : ICommand<OK>
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}
