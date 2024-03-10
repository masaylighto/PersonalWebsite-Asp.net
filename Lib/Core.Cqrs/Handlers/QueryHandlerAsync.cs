#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
using Core.Cqrs.Requests;
using Core.DataKit.Result;
using Cqrs.Handlers;

namespace Core.Cqrs.Handlers;

public abstract class QueryHandler<Query, Response> : BaseHandler<Query, Response> where Query : IQuery<Response>
{

    protected override async Task<Result<Response>> Execute(Query request, CancellationToken cancellationToken)
    {
        return Fetch(request, cancellationToken);
    }
    protected abstract Result<Response> Fetch(Query request, CancellationToken cancellationToken);
}