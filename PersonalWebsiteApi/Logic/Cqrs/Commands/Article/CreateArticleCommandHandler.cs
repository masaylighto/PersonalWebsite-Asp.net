
using Core.Cqrs.Handlers;
using Core.DataKit.Result;
using Core.LinqExtensions;
using FluentValidation;
using PersonalWebsiteApi.Core.Cqrs.Commands.Article;
using PersonalWebsiteApi.Core.Cqrs.Responses;
using PersonalWebsiteApi.Core.Entities;
using PersonalWebsiteApi.Core.Exceptions;
using PersonalWebsiteApi.Core.Helpers;
using PersonalWebsiteApi.Core.Loggers;
using PersonalWebsiteApi.DataAccess.Interfaces;
using Entities=PersonalWebsiteApi.Core.Entities;
namespace PersonalWebsiteApi.Logic.Cqrs.Commands.Article;

public class CreateArticleCommandHandler : CommandHandler<CreateArticleCommand, CreateArticleCommandResponse>
{
    public IUnitOfWork UnitOfWork { get; }
    public ArticlesHandler ArticlesHandler { get; }
    public ILog Log { get; }

    class CommandValidator : AbstractValidator<CreateArticleCommand>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Title).MinimumLength(1);
            RuleFor(x => x.Overview).MinimumLength(5);
            RuleFor(x => x.Content).MinimumLength(100);
            RuleFor(x => x.CategoryID).NotEmpty();
        }
    }
    public CreateArticleCommandHandler(IUnitOfWork unitOfWork, ArticlesHandler articlesHandler,ILog log)
    {
        Validator = new CommandValidator();
        UnitOfWork = unitOfWork;
        ArticlesHandler = articlesHandler;
        Log = log;
    }
    protected override async Task<Result<CreateArticleCommandResponse>> Execute(CreateArticleCommand request, CancellationToken cancellationToken)
    {

        Guid ID = Guid.NewGuid();
        var isAlreadyExist = await UnitOfWork.ArticleRepository.IsExistAsync(x => request.Title==x.Title & x.Category.Id == request.CategoryID);
        if (isAlreadyExist.ContainError())
        {
            return isAlreadyExist.GetError();
        }

        if (isAlreadyExist.GetData())
        {
            return new UniqueFieldException("Article already Exist");
        }

        var autherResult = await UnitOfWork.UserRespository.GetAsync();
        if (autherResult.ContainError())
        {
            return autherResult.GetError();
        }

        var categoryResult = await UnitOfWork.CategoriesRepository.GetAsync(x => x.Id == request.CategoryID);
        if (categoryResult.ContainError())
        {
            return categoryResult.GetError();
        }

        ArticlesHandler.LoadArticleContent(request.Content);

        var seperatedImageResult = await ArticlesHandler.SeparateImage();        
        if (seperatedImageResult.IsNotOk())
        {
            Log.Error(seperatedImageResult.GetError());
            return seperatedImageResult.GetError();
        }

        var articleContentResult =  ArticlesHandler.GetArticleContent();
        if (articleContentResult.ContainError())
        {
            Log.Error(articleContentResult.GetError());
            return articleContentResult.GetError();
        }

        Entities.Article article = new()
        {
            Title = request.Title,
            Overview = request.Overview,
            Content = articleContentResult.GetData(),
            Id = ID,
            Pircture=request.Picture,
            Auther = autherResult.GetData(),
            Category = categoryResult.GetData(),
        };

        var result = await UnitOfWork.ArticleRepository.AddAsync(article);
        if (result.IsNotOk())
        {
            return result.GetError();
        }
        var saveResult=await UnitOfWork.SaveAsync();
        if (saveResult.ContainError())
        {
            return saveResult.GetError();
        }
        return new CreateArticleCommandResponse() { Id = ID };
    }
    
}
