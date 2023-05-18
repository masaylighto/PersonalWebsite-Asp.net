using System.ComponentModel.DataAnnotations;

namespace TheWayToGerman.Api.DTO.Owner;

public class DeleteAdminDTO
{
    [Required]
    public Guid Id  { get; set; }
}
