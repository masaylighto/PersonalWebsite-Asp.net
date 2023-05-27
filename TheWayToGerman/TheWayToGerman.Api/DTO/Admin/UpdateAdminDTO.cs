
using Microsoft.AspNetCore.Mvc;
using TheWayToGerman.Core.Helpers;
using TheWayToGerman.Core.ParametersBinders;

namespace TheWayToGerman.Api.DTO.Admin;

public class UpdateAdminDTO
{

 
   
    public required string Name { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}
