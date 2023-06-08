

using Core.Cqrs.Handlers;
using Core.DataKit;
using Core.DataKit.Result;
using Core.Expressions;
using FluentValidation;
using Mapster;
using System.Linq.Expressions;
using TheWayToGerman.Core.Cqrs.Commands;
using TheWayToGerman.Core.Entities;
using TheWayToGerman.Core.Enums;
using TheWayToGerman.Core.Exceptions;
using TheWayToGerman.DataAccess.Interfaces;

namespace TheWayToGerman.Logic.Cqrs.Commands;

public class CreateFirstOwnerCommandHandler : CommandHandler<CreateFirstOwnerCommand, OK>
{
    public IUnitOfWork UnitOfWork { get; }

    class CreateFirstOwnerValidator : AbstractValidator<CreateFirstOwnerCommand>
    {
        public CreateFirstOwnerValidator()
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
        Validator = new CreateFirstOwnerValidator();
        UnitOfWork = unitOfWork;
    }
    protected override async Task<Result<OK>> Execute(CreateFirstOwnerCommand request, CancellationToken cancellationToken)
    {
        var result = await UnitOfWork.UserRespository.IsUserExistAsync(x=> x.UserType == UserType.Owner);
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
        user.UserType = Core.Enums.UserType.Owner;
        var addResult = await UnitOfWork.UserRespository.AddUserAsync(user);
        if (addResult.ContainError())
        {
            return addResult.GetError();
        }
        var saveResult = await UnitOfWork.SaveAsync();
        return saveResult;       
    }
}
