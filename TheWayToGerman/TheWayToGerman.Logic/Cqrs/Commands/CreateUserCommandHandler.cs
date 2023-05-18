

using Core.Cqrs.Handlers;
using Core.DataKit;
using Core.DataKit.Result;
using Core.Expressions;
using FluentValidation;
using Mapster;
using System.Linq.Expressions;
using TheWayToGerman.Core.Cqrs.Commands;
using TheWayToGerman.Core.Entities;
using TheWayToGerman.Core.Exceptions;
using TheWayToGerman.DataAccess.Interfaces;

namespace TheWayToGerman.Logic.Cqrs.Commands;

public class CreateUserCommandHandler : CommandHandler<CreateUserCommand, OK>
{
    public IUnitOfWork UnitOfWork { get; }

    class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Username).MinimumLength(1);
            RuleFor(x => x.Password).MinimumLength(8);
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Username).MinimumLength(1);
            RuleFor(x => x.Name).MinimumLength(1);
        }
    }
    public CreateUserCommandHandler(IUnitOfWork unitOfWork)
    {
        Validator = new CreateUserValidator();
        UnitOfWork = unitOfWork;
    }
    protected override async Task<Result<OK>> Execute(CreateUserCommand request, CancellationToken cancellationToken)
    {
        Expression<Func<string, bool>> expression = (text) => text.Contains('1') || text.Contains('2');

        expression.Not();
        User user = request.Adapt<User>();
        user.SetPassword(request.Password);
        var addResult = await UnitOfWork.UserRespository.AddUserAsync(user);
        if (addResult.ContainError())
        {
            return addResult.GetError();
        }
        var saveResult = await UnitOfWork.SaveAsync();
        return saveResult;       
    }
}
