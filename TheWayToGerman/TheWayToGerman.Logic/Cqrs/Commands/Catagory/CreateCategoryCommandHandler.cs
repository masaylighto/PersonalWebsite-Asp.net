using Core.Cqrs.Handlers;
using Core.DataKit.Result;
using FluentValidation;
using TheWayToGerman.Core.Cqrs.Commands;
using TheWayToGerman.Core.Cqrs.Responses;
using TheWayToGerman.Core.Entities;
using TheWayToGerman.DataAccess.Interfaces;

namespace TheWayToGerman.Logic.Cqrs.Commands;

public class CreateCategoryCommandHandler : CommandHandler<CreateCategoryCommand, CreateCategoryCommandResponse>
{
    public IUnitOfWork UnitOfWork { get; }

    class CommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Name).MinimumLength(1);
        }
    }
    public CreateCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        Validator = new CommandValidator();
        UnitOfWork = unitOfWork;
    }
    protected override async Task<Result<CreateCategoryCommandResponse>> Execute(CreateCategoryCommand request, CancellationToken cancellationToken)
    {


        var category = new Category()
        {
            Id = Guid.NewGuid(),
            Language = request.Language,
            Name = request.Name,
        };

        var addResult = await UnitOfWork.CategoriesRepository.AddAsync(category);
        if (addResult.ContainError())
        {
            return addResult.GetError();
        }
        var saveResult = await UnitOfWork.SaveAsync();
        if (saveResult.ContainError())
        {
            return saveResult.GetError();
        }

        return new CreateCategoryCommandResponse()
        {
            Id = category.Id
        };
    }
}
