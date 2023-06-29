using Core.Cqrs.Handlers;
using Core.DataKit;
using Core.DataKit.Result;
using Core.Expressions;
using FluentValidation;
using Mapster;
using System.Linq.Expressions;
using TheWayToGerman.Core.Cqrs.Commands.Admin;
using TheWayToGerman.Core.Entities;
using TheWayToGerman.Core.Exceptions;
using TheWayToGerman.DataAccess.Interfaces;

namespace TheWayToGerman.Logic.Cqrs.Commands;

public class UpdateAdminCommandHandler : CommandHandler<UpdateAdminCommand, OK>
{
    public IUnitOfWork UnitOfWork { get; }

    class CommandValidator : AbstractValidator<UpdateAdminCommand>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Id).NotEqual(new Guid());
            RuleFor(x => x.Username).MinimumLength(1);
            RuleFor(x => x.Password).MinimumLength(8);
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Username).MinimumLength(1);
            RuleFor(x => x.Name).MinimumLength(1);
        }
    }
    public UpdateAdminCommandHandler(IUnitOfWork unitOfWork)
    {
        Validator = new CommandValidator();
        UnitOfWork = unitOfWork;
    }
    protected override async Task<Result<OK>> Execute(UpdateAdminCommand request, CancellationToken cancellationToken)
    {
        User user = request.Adapt<User>();
        user.UserType = Core.Enums.UserType.Admin;
        user.SetPassword(request.Password);
        var addResult = await UnitOfWork.UserRespository.UpdateUserAsync(user,x=>x.Id==user.Id);
        if (addResult.ContainError())
        {
            return addResult.GetError();
        }
        var saveResult = await UnitOfWork.SaveAsync();
        return saveResult;       
    }
}
