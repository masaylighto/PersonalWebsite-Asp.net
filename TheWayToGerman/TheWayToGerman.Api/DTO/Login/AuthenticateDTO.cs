using System.ComponentModel.DataAnnotations;

namespace TheWayToGerman.Api.DTO.Login
{
    public class AuthenticateDTO
    {

        [Required]
        public required string Username { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
