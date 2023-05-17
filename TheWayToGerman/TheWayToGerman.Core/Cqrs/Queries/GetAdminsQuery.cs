using Core.Cqrs.Requests;
using TheWayToGerman.Core.Entities;

namespace TheWayToGerman.Core.Cqrs.Queries;

public class GetAdminsQuery : IQuery<IEnumerable<GetAdminsQueryResponse>>
{
    public string? Name { get; set; }
}
