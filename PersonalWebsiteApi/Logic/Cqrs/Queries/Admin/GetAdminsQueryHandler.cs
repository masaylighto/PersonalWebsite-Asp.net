using Core.Cqrs.Handlers;
using Core.DataKit.Result;
using Core.Expressions;
using FluentValidation;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using PersonalWebsiteApi.Core.Cqrs.Queries;
using PersonalWebsiteApi.Core.Entities;
using PersonalWebsiteApi.Core.Enums;
using PersonalWebsiteApi.DataAccess.Interfaces;

namespace PersonalWebsiteApi.Logic.Cqrs.Queries;

public class GetAdminsQueryHandler : QueryHandler<GetAdminsQuery, IAsyncEnumerable<GetAdminsQueryResponse>>
{
    public IUnitOfWork UnitOfWork { get; }

    class CommandValidator : AbstractValidator<GetAdminsQuery>
    {
        public CommandValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1).LessThanOrEqualTo(20);
        }
    }
    public GetAdminsQueryHandler(IUnitOfWork unitOfWork)
    {
        Validator = new CommandValidator();
        UnitOfWork = unitOfWork;
    }

    protected override Result<IAsyncEnumerable<GetAdminsQueryResponse>> Fetch(GetAdminsQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<User, bool>> filter = (User user) => user.UserType == UserType.Admin;
        if (request.Name is not null)
        {
            string searchPattern = $"%{request.Name.ToLower()}%";
            filter = filter.And(User =>  EF.Functions.Like(User.Name.ToLower(), searchPattern));
        }
        return UnitOfWork.UserRespository.GetUsers(filter,
            GetAdminsQueryResponse.From,
            request.PageSize,
            request.PageNumber);
    }
}
