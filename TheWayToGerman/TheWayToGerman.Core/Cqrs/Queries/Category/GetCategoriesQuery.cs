
using Core.Cqrs.Requests;
using TheWayToGerman.Core.Cqrs.Responses;
using TheWayToGerman.Core.Enums;

namespace TheWayToGerman.Core.Cqrs.Queries.Category;

public class GetCategoriesQuery : IQuery<IEnumerable<GetCategoriesQueryResponse>>
{
    public string? Name { get; set; }
    public Language? Language { get; set; }
}
