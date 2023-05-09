using System.ComponentModel.DataAnnotations;
using TheWayToGerman.Core.Enums;

namespace TheWayToGerman.Api.DTO.Owner;

public class CreateAdminDTO
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Username { get; set; }
    [Required, EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
