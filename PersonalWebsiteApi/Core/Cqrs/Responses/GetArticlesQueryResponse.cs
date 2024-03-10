
using PersonalWebsiteApi.Core.Entities;

namespace PersonalWebsiteApi.Core.Cqrs.Responses;

public class GetArticlesQueryResponse
{
    public required Guid ID { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }
    public required string Category { get; set; }
    public required string Picture { get; set; }
    public required string Summary { get; set; }
    public required DateTime CreateDate { get; set; }
    public static GetArticlesQueryResponse From(Article article) => new()
    {
        ID = article.Id,
        Title = article.Title,
        Picture = article.Pircture,
        Summary = article.Overview,
        CreateDate = article.CreateDate,
        Author = article.Auther.Name,
        Category= article.Category.Name
    };
}
