﻿using Core.Cqrs.Handlers;
using Core.DataKit.Result;
using Core.Expressions;
using FluentValidation;
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

    protected override Result<IEnumerable<GetAdminsQueryResponse>> Fetch(GetAdminsQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<User, bool>> filter = (User user) => user.UserType == UserType.Admin;
        if (request.Name is not null)
        {
            filter = filter.And(User => User.Name.Contains(request.Name, StringComparison.InvariantCultureIgnoreCase));
        }
        return UnitOfWork.UserRespository.GetUsers(filter, (user) => user.Adapt<GetAdminsQueryResponse>(), request.PageSize, request.PageNumber);
    }
}
