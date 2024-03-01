using Core.Cqrs.Handlers;
using Core.DataKit.Result;
using FluentValidation;
using PersonalWebsiteApi.Core.Cqrs.Queries;
using PersonalWebsiteApi.Core.Entities;
using PersonalWebsiteApi.Core.Exceptions;
using PersonalWebsiteApi.DataAccess.Interfaces;

namespace PersonalWebsiteApi.Logic.Cqrs.Queries;

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
        var userResult= await UnitOfWork.UserRespository.GetAsync();
        if (userResult.ContainError() ) {
            return userResult.GetError();
        }
        var user = userResult.GetData();
        if (user.Username != request.Username)
        {
            return new DataNotFoundException("no user with this username");
        }
        if (!user.IsPasswordEqual(request.Password))
        {
            return new DataNotFoundException("no user with this password");
        }
        return user;
    }
}

