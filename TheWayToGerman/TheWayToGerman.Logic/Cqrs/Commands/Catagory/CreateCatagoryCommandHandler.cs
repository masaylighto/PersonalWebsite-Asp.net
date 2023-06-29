using Core.Cqrs.Handlers;
using Core.DataKit.Result;
using FluentValidation;
using TheWayToGerman.Core.Cqrs.Commands;
using TheWayToGerman.Core.Cqrs.Responses;
using TheWayToGerman.Core.Entities;
using TheWayToGerman.DataAccess.Interfaces;

namespace TheWayToGerman.Logic.Cqrs.Commands;

public class CreateCatagoryCommandHandler : CommandHandler<CreateCatagoryCommand, CreateCatagoryCommandResponse>
{
    public IUnitOfWork UnitOfWork { get; }

    class CommandValidator : AbstractValidator<CreateCatagoryCommand>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Name).MinimumLength(1);
            RuleFor(x => x.LanguageID).NotEqual(new Guid());
        }
    }
    public CreateCatagoryCommandHandler(IUnitOfWork unitOfWork)
    {
        Validator = new CommandValidator();
        UnitOfWork = unitOfWork;
    }
    protected override async Task<Result<CreateCatagoryCommandResponse>> Execute(CreateCatagoryCommand request, CancellationToken cancellationToken)
    {
        var languageResult= await UnitOfWork.LanguageRepository.GetAsync(x => x.Id == request.LanguageID);
        if (languageResult.ContainError())
        {
            return languageResult.GetError();
        }
   
        var catagory = new Category() { 
            Id = Guid.NewGuid(),
            Language = languageResult.GetData(),
            Name = request.Name,    
        };

        var addResult = await UnitOfWork.CatagoriesRepository.AddAsync(catagory);
        if (addResult.ContainError())
        {
            return addResult.GetError();
        }
        var saveResult = await UnitOfWork.SaveAsync();
        if (saveResult.ContainError())
        {
            return saveResult.GetError();
        }

        return new CreateCatagoryCommandResponse() { 
            Id  = catagory.Id
        };       
    }
}
