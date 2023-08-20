using Core.Cqrs.Requests;
using TheWayToGerman.Core.Cqrs.Responses;

namespace TheWayToGerman.Core.Cqrs.Queries.Article;

public class GetArticleQuery:IQuery<GetArticleQueryResponse>
{
    public string Title { get; set;  }
    public string Description { get; set; }
    public IEnumerable<Guid> Categories { get; set; }
}
