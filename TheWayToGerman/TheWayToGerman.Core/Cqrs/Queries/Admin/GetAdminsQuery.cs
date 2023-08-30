using Core.Cqrs.Requests;

namespace TheWayToGerman.Core.Cqrs.Queries;

public class GetAdminsQuery : IQuery<IAsyncEnumerable<GetAdminsQueryResponse>>
{
    public string? Name { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
