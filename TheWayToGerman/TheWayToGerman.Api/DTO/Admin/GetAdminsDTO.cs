using System.ComponentModel.DataAnnotations;

namespace TheWayToGerman.Api.DTO.Admin;

public class GetAdminsDTO
{
    public string? Name { get; set; }
    public int PageNumber { get; set; } = 0;
    public int PageSize { get; set; } = 20;
}

