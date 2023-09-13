using TheWayToGerman.Core.Entities;
using TheWayToGerman.Core.Enums;

namespace TheWayToGerman.Core.Cqrs.Responses;

public class GetCategoriesQueryResponse
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required Language Language { get; set; }

}
