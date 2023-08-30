
using Core.Cqrs.Requests;
using TheWayToGerman.Core.Cqrs.Responses;
using TheWayToGerman.Core.Enums;

namespace TheWayToGerman.Core.Cqrs.Queries.Category;

public class GetCategoriesQuery : IQuery<IAsyncEnumerable<GetCategoriesQueryResponse>>
{
    public Language? Language { get; set; } = Enums.Language.Arabic;
}
