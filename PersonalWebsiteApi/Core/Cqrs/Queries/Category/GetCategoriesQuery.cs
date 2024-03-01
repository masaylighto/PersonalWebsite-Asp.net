
using Core.Cqrs.Requests;
using PersonalWebsiteApi.Core.Cqrs.Responses;
using PersonalWebsiteApi.Core.Enums;

namespace PersonalWebsiteApi.Core.Cqrs.Queries.Category;

public class GetCategoriesQuery : IQuery<IAsyncEnumerable<GetCategoriesQueryResponse>>
{
    public Language? Language { get; set; } = Enums.Language.Arabic;
}
