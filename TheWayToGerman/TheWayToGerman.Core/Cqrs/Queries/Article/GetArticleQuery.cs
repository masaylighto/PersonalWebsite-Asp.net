using Core.Cqrs.Requests;
using TheWayToGerman.Core.Cqrs.Responses;

namespace TheWayToGerman.Core.Cqrs.Queries.Article;

public class GetArticlesQuery :IQuery<IEnumerable<GetArticlesQueryResponse>>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid CategoryID { get; set; }
}
