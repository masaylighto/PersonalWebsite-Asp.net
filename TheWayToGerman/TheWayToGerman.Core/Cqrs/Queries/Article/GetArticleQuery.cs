using Core.Cqrs.Requests;
using TheWayToGerman.Core.Cqrs.Responses;

namespace TheWayToGerman.Core.Cqrs.Queries.Article;

public class GetArticlesQuery :IQuery<IAsyncEnumerable<GetArticlesQueryResponse>>
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public Guid? CategoryID { get; set; }
    public int PageLength { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
}
