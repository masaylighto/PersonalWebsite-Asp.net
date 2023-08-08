using Core.Cqrs.Handlers;
using Core.DataKit.Result;
using FluentValidation;
using Mapster;
using TheWayToGerman.Core.Cqrs.Commands.Admin;
using TheWayToGerman.Core.Cqrs.Queries;
using TheWayToGerman.Core.Entities;
using TheWayToGerman.DataAccess.Interfaces;

namespace TheWayToGerman.Logic.Cqrs.Commands.Admin;

public class CreateAdminCommandHandler : CommandHandler<CreateAdminCommand, CreateAdminCommandResponse>
{
    public IUnitOfWork UnitOfWork { get; }

    class CommandValidator : AbstractValidator<CreateAdminCommand>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Username).MinimumLength(1);
            RuleFor(x => x.Password).MinimumLength(8);
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Username).MinimumLength(1);
            RuleFor(x => x.Name).MinimumLength(1);
        }
    }
    public CreateAdminCommandHandler(IUnitOfWork unitOfWork)
    {
        Validator = new CommandValidator();
        UnitOfWork = unitOfWork;
    }
    protected override async Task<Result<CreateAdminCommandResponse>> Execute(CreateAdminCommand request, CancellationToken cancellationToken)
    {
        User user = request.Adapt<User>();
        user.UserType = Core.Enums.UserType.Admin;
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

        return new CreateAdminCommandResponse()
        {
            Id = addResult.GetData()
        };
    }
}
