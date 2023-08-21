using Core.Cqrs.Requests;
using TheWayToGerman.Core.Cqrs.Responses;

namespace TheWayToGerman.Core.Cqrs.Queries.Article;

public class GetArticleQuery:IQuery<GetArticleQueryResponse>
{
    public Guid ID { get; set; }

}
