using Core.Cqrs.Requests;
using TheWayToGerman.Core.Cqrs.Responses;

namespace TheWayToGerman.Core.Cqrs.Queries.Article;

public class GetArticlesQuery :IQuery<GetArticlesQueryResponse>
{
    public Guid ID { get; set; }
}
