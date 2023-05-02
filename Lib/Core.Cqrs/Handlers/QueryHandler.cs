

using Core.Cqrs.Requests;
using Core.DataKit.Result;
using Cqrs.Handlers;

namespace Core.Cqrs.Handlers;

public abstract class QueryHandler<Query, Response> : BaseHandler<Query, Response> where Query : IQuery<Response>
{
    protected override async Task<Result<Response>> Execute(Query request, CancellationToken cancellationToken)
    {
        return await Fetch(request, cancellationToken);
    }
    protected abstract Task<Result<Response>> Fetch(Query request, CancellationToken cancellationToken);
}