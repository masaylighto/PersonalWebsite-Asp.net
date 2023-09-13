
using TheWayToGerman.Core.Entities;

namespace TheWayToGerman.Core.Cqrs.Responses;

public class GetArticlesQueryResponse
{
    public required Guid ID { get; set; }
    public required string Title { get; set; }
    public required string Picture { get; set; }
    public required string Summary { get; set; }
    public static GetArticlesQueryResponse From(Article article) => new()
    {
        ID = article.Id,
        Title = article.Title,
        Picture = article.Pircture,
        Summary = article.Overview
    };
}
