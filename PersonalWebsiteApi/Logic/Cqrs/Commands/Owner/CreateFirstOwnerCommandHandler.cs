

using Core.Cqrs.Handlers;
using Core.DataKit;
using Core.DataKit.Result;
using FluentValidation;
using Mapster;
using PersonalWebsiteApi.Core.Cqrs.Commands;
using PersonalWebsiteApi.Core.Entities;
using PersonalWebsiteApi.Core.Enums;
using PersonalWebsiteApi.Core.Exceptions;
using PersonalWebsiteApi.DataAccess.Interfaces;

namespace PersonalWebsiteApi.Logic.Cqrs.Commands;

public class CreateFirstOwnerCommandHandler : CommandHandler<CreateFirstOwnerCommand, OK>
{
    public IUnitOfWork UnitOfWork { get; }

    class CommandValidator : AbstractValidator<CreateFirstOwnerCommand>
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
    public CreateFirstOwnerCommandHandler(IUnitOfWork unitOfWork)
    {
        Validator = new CommandValidator();
        UnitOfWork = unitOfWork;
    }
    protected override async Task<Result<OK>> Execute(CreateFirstOwnerCommand request, CancellationToken cancellationToken)
    {
        var result = await UnitOfWork.UserRespository.IsOwnerExistAsync();
        if (result.IsDataType<OK>())
        {
            return new UniqueFieldException("there should be only one owner to the system");
        }
        if (result.IsInternalError())
        {
            return result.GetError();
        }
        User user = request.Adapt<User>();
        user.SetPassword(request.Password);
        var addResult = await UnitOfWork.UserRespository.AddAsync(user);
        if (addResult.ContainError())
        {
            return addResult.GetError();
        }
        var saveResult = await UnitOfWork.SaveAsync();
        return saveResult;
    }
}
