
using Core.Cqrs.Requests;
using Core.DataKit;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using TheWayToGerman.Core.Enums;

namespace TheWayToGerman.Core.Cqrs.Commands;

public class CreateUserCommand:ICommand<OK>
{   
    public required string Name { get; set; }    
    public required string Username { get; set; }
    public required string Email { get; set; }  
    public required string Password { get; set; }
    public required UserType UserType { get; set; } = UserType.Admin;
}
