using Core.Cqrs.Handlers;
using Core.DataKit.Result;
using FluentValidation;
using PersonalWebsiteApi.Core.Cqrs.Commands;
using PersonalWebsiteApi.Core.Cqrs.Responses;
using PersonalWebsiteApi.Core.Entities;
using PersonalWebsiteApi.DataAccess.Interfaces;

namespace PersonalWebsiteApi.Logic.Cqrs.Commands;

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
