
namespace TheWayToGerman.Core.Cqrs.Responses;

public class GetArticlesQueryResponse
{
    public required Guid ID { get; set; }
    public required string Title { get; set; }
    public required string Summary { get; set; }
}
