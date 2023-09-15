
using TheWayToGerman.Core.Entities;

namespace TheWayToGerman.Core.Cqrs.Responses;

public class GetArticleQueryResponse
{
    public required Guid ID { get; set; }
    public required string Title { get; set; }
    public required string Picture { get; set; }
    public required string Overview { get; set; }
    public required string Content { get; set; }
    public required DateTime CreateDate { get; set; }
    public required string Author { get; set; }
    public required string Category { get; set; }
    public static GetArticleQueryResponse From(Article article) => new()
    {
        Content = article.Content,
        Overview = article.Overview,
        Title = article.Title,
        ID = article.Id,
        Picture = article.Pircture,
        CreateDate = article.CreateDate,
        Author = article.Auther.Name,
        Category=article.Category.Name
        

    };
}
