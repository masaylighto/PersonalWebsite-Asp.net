using System.ComponentModel.DataAnnotations;

namespace TheWayToGerman.Api.DTO;

public class UpdateUserInformationDTO
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
