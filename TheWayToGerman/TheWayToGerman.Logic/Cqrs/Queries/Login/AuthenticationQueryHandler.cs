using Core.Cqrs.Handlers;
using Core.DataKit.Result;
using FluentValidation;
using TheWayToGerman.Core.Cqrs.Queries;
using TheWayToGerman.Core.Entities;
using TheWayToGerman.DataAccess.Interfaces;

namespace TheWayToGerman.Logic.Cqrs.Queries;

public class AuthenticationQueryHandler : QueryHandlerAsync<AuthenticationQuery, User>
{
    class CommandValidator : AbstractValidator<AuthenticationQuery>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Username).MinimumLength(1);
            RuleFor(x => x.Password).MinimumLength(1);
        }
    }
    public IUnitOfWork UnitOfWork { get; }
    public AuthenticationQueryHandler(IUnitOfWork unitOfWork)
    {
        Validator = new CommandValidator();
        UnitOfWork = unitOfWork;
    }

    protected override async Task<Result<User>> Fetch(AuthenticationQuery request, CancellationToken cancellationToken)
    {
        return await UnitOfWork.UserRespository.GetAsync(x => x.Username == request.Username && x.IsPasswordEqual(request.Password));
    }
}

