

using Core.Cqrs.Handlers;
using Core.DataKit;
using Core.DataKit.Result;
using FluentValidation;
using Mapster;
using PersonalWebsiteApi.Core.Cqrs.Commands;
using PersonalWebsiteApi.Core.Entities;
using PersonalWebsiteApi.DataAccess.Interfaces;

namespace PersonalWebsiteApi.Logic.Cqrs.Commands;

public class UpdateOwnerCommandHandler : CommandHandler<UpdateOwnerInformationCommand, OK>
{
    public IUnitOfWork UnitOfWork { get; }

    class CommandValidator : AbstractValidator<UpdateOwnerInformationCommand>
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
    public UpdateOwnerCommandHandler(IUnitOfWork unitOfWork)
    {
        Validator = new CommandValidator();
        UnitOfWork = unitOfWork;
    }
    protected override async Task<Result<OK>> Execute(UpdateOwnerInformationCommand request, CancellationToken cancellationToken)
    {
        User user = request.Adapt<User>();
        user.SetPassword(request.Password);
        var getOwnerResult = await UnitOfWork.UserRespository.UpdateAsync(user);
        if (getOwnerResult.ContainError())
        {
            return getOwnerResult.GetError();
        }
        var saveResult = await UnitOfWork.SaveAsync();
        return saveResult;

    }
}
