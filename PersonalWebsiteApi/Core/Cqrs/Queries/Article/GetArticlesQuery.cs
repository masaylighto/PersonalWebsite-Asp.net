using Core.Cqrs.Requests;
using PersonalWebsiteApi.Core.Cqrs.Responses;

namespace PersonalWebsiteApi.Core.Cqrs.Queries.Article;

public class GetArticleQuery:IQuery<GetArticleQueryResponse>
{
 
    public Guid ID { get; set; }

}
