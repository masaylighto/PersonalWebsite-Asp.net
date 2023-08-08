using Core.Cqrs.Requests;

namespace TheWayToGerman.Core.Cqrs.Queries;

public class GetAdminsQuery : IQuery<IEnumerable<GetAdminsQueryResponse>>
{
    public string? Name { get; set; }
    public int PageNumber { get; set; } = 0;
    public int PageSize { get; set; } = 20;
}
