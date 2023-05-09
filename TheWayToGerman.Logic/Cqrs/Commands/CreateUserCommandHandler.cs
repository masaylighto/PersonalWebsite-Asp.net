

using Core.Cqrs.Handlers;
using Core.DataKit;
using Core.DataKit.Result;
using FluentValidation;
using Mapster;
using TheWayToGerman.Core.Cqrs.Commands;
using TheWayToGerman.Core.Cqrs.Queries;
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
        User user = request.Adapt<User>();
        user.SetPassword(request.Password);
        var addResult = await UnitOfWork.UserRespository.AddUserAsync(user);
        if (addResult.ContainError())
        {
            return addResult.GetError();
        }
        var saveResult = await UnitOfWork.SaveAsync();
        if (saveResult.ContainError())
        {
            return saveResult.GetError();
        }
        if (saveResult.GetData())
        {
            return new OK();
        }
        return new DBNoChangesException("Create user opeartion result in no changes in the database");

    }
}
