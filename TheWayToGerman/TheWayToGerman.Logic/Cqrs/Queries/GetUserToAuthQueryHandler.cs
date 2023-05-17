using Core.Cqrs.Handlers;
using Core.DataKit.Result;
using FluentValidation;
using TheWayToGerman.Core.Cqrs.Queries;
using TheWayToGerman.Core.Entities;
using TheWayToGerman.DataAccess.Interfaces;

namespace TheWayToGerman.Logic.Cqrs.Queries;

public class GetUserToAuthQueryHandler : QueryHandler<GetUserToAuthQuery, User>
{
    class UserValidator : AbstractValidator<GetUserToAuthQuery>
    {
        public UserValidator()
        {
            RuleFor(x => x.Username).MinimumLength(1);
            RuleFor(x => x.Password).MinimumLength(1);
        }
    }
    public IUnitOfWork UnitOfWork { get; }
    public GetUserToAuthQueryHandler(IUnitOfWork unitOfWork)
    {
        Validator = new UserValidator();
        UnitOfWork = unitOfWork;
    }

    protected override async Task<Result<User>> Fetch(GetUserToAuthQuery request, CancellationToken cancellationToken)
    {
        return await UnitOfWork.UserRespository.GetUserAsync(x => x.Username == request.Username && x.IsPasswordEqual(request.Password));
    }
}

