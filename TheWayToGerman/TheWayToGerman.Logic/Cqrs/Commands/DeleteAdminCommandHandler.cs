

using Core.Cqrs.Handlers;
using Core.DataKit;
using Core.DataKit.Result;
using FluentValidation;
using Mapster;
using TheWayToGerman.Core.Cqrs.Commands;
using TheWayToGerman.Core.Entities;
using TheWayToGerman.Core.Exceptions;
using TheWayToGerman.DataAccess.Interfaces;

namespace TheWayToGerman.Logic.Cqrs.Commands;

public class DeleteAdminCommandHandler : CommandHandler<DeleteAdminCommand, OK>
{
    public IUnitOfWork UnitOfWork { get; }

  
    public DeleteAdminCommandHandler(IUnitOfWork unitOfWork)
    {     
        UnitOfWork = unitOfWork;
    }
    protected override async Task<Result<OK>> Execute(DeleteAdminCommand request, CancellationToken cancellationToken)
    {
     
        var addResult = await UnitOfWork.UserRespository.DeleteAdminById(request.Id);
        if (addResult.ContainError())
        {
            return addResult.GetError();
        }
        var saveResult = await UnitOfWork.SaveAsync(); 
        return saveResult;

    }
}
