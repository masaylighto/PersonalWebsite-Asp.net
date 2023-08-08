﻿
using Core.Cqrs.Requests;
using TheWayToGerman.Core.Cqrs.Queries;

namespace TheWayToGerman.Core.Cqrs.Commands.Admin;

public class CreateAdminCommand : ICommand<CreateAdminCommandResponse>
{
    public required string Name { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}
