
namespace TheWayToGerman.Core.Cqrs.Responses;

public class GetArticleQueryResponse
{
    public required Guid ID { get; set; }
    public required string Title { get; set; }
    public required string Overview { get; set; }
    public required string Content { get; set; }
}
