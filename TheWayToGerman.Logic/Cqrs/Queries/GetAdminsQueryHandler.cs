using Core.Cqrs.Handlers;
using Core.DataKit.Result;
using Core.Expressions;
using Mapster;
using System.Linq.Expressions;
using TheWayToGerman.Core.Cqrs.Queries;
using TheWayToGerman.Core.Entities;
using TheWayToGerman.Core.Enums;
using TheWayToGerman.DataAccess.Interfaces;

namespace TheWayToGerman.Logic.Cqrs.Queries;

public class GetAdminsQueryHandler : QueryHandler<GetAdminsQuery, IEnumerable<GetAdminsQueryResponse>>
{
    public IUnitOfWork UnitOfWork { get; }
    public GetAdminsQueryHandler(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    protected override async Task<Result<IEnumerable<GetAdminsQueryResponse>>> Fetch(GetAdminsQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<User, bool>> filter = (User user) => user.UserType == UserType.Admin;
        if (request.Name is not null)
        {
            filter = filter.And(User => User.Name.Contains(request.Name, StringComparison.InvariantCultureIgnoreCase));
        }
        return await UnitOfWork.UserRespository.GetUsersAsync(filter,(user)=> user.Adapt<GetAdminsQueryResponse>());
    }
}
